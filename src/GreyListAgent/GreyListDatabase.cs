using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace GreyListAgent
{
    [XmlRoot("GreyListDatabase")]
    public class GreyListDatabase : OrderedDictionary, IXmlSerializable
    {
        /// <summary>
        ///     Last index that was cleaned
        /// </summary>
        private int _lastCleanIndex;

        /// <summary>
        ///     Schema overload
        /// </summary>
        /// <returns>Null</returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        ///     XMLReader overload for reading saved serialized XML files
        /// </summary>
        /// <param name="reader">XMLReader provided for deserializing</param>
        public void ReadXml(XmlReader reader)
        {
            var keySerializer = new XmlSerializer(typeof (string));
            var valueSerializer = new XmlSerializer(typeof (GreyListEntry));
            var wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
                return;
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("entry");
                reader.ReadStartElement("hash");
                var key = (string) keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                var value = (GreyListEntry) valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        /// <summary>
        ///     XMLWriter overload for writing serialized objects
        /// </summary>
        /// <param name="writer">XMLWriter for serializing objects</param>
        public void WriteXml(XmlWriter writer)
        {
            var keySerializer = new XmlSerializer(typeof (string));
            var valueSerializer = new XmlSerializer(typeof (GreyListEntry));
            foreach (string key in Keys)
            {
                writer.WriteStartElement("entry");
                writer.WriteStartElement("hash");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                var value = (GreyListEntry) this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        /// <summary>
        ///     Loads
        /// </summary>
        /// <param name="path"></param>
        /// <returns>loaded database or empty database if it can't load the database</returns>
        public static GreyListDatabase Load(string path)
        {
            var serializer = new XmlSerializer(typeof (GreyListDatabase));
            GreyListDatabase db;
            try
            {
                using (var source = new StreamReader(File.OpenRead(path)))
                {
                    db = (GreyListDatabase) serializer.Deserialize(source);
                }
            }
            catch (IOException e)
            {
                Debug.WriteLine(e.ToString());
                return new GreyListDatabase();
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e.ToString());
                return new GreyListDatabase();
            }
            catch (SerializationException e)
            {
                Debug.WriteLine(e.ToString());
                return new GreyListDatabase();
            }
            return db;
        }

        /// <summary>
        ///     Saves the database to specified path
        /// </summary>
        /// <param name="path">Path to save the database to</param>
        public void Save(string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof (GreyListDatabase));

                using (var outputStream = new StreamWriter(File.Open(path, FileMode.Create)))
                {
                    lock (((IOrderedDictionary) this).SyncRoot)
                    {
                        serializer.Serialize(outputStream, this);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.ToString());
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e.ToString());
            }
            catch (IOException e)
            {
                Debug.WriteLine(e.ToString());
            }
            catch (SerializationException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        /// <summary>
        ///     Cleans the database. Examines cleanRowCount rows at a time. Removes unconfirmed entries older than
        ///     unconfirmedMaxAge and removes confirmed entries older than confirmedMaxAge
        /// </summary>
        /// <param name="cleanRowCount">Number of rows to clean per call</param>
        /// <param name="confirmedMaxAge">Maximum age of confirmed entries</param>
        /// <param name="unconfirmedMaxAge">Maximum age of uconfirmed entries</param>
        public void Clean(int cleanRowCount, TimeSpan confirmedMaxAge, TimeSpan unconfirmedMaxAge)
        {
            lock (((IOrderedDictionary) this).SyncRoot)
            {
                // If we don't have any items just return
                if (Count < 1)
                {
                    return;
                }

                // If we are at the end of the list, or are at greater than the end of the list, wrap around
                if (_lastCleanIndex >= Count)
                {
                    _lastCleanIndex = 0;
                }

                // If we are trying to clean more items than we have, adjust so we clean what we do have
                if (cleanRowCount + _lastCleanIndex > Count)
                {
                    cleanRowCount = Count - _lastCleanIndex;
                }
                var lastIndex = _lastCleanIndex + cleanRowCount;
                if (lastIndex > Count)
                {
                    lastIndex = Count;
                }
                // Loop through the section of ourselves, get a list of keys to remove, then remove them
                var indexesToClean = new List<string>();
                var now = DateTime.UtcNow;
                var keys = new object[Keys.Count];
                Keys.CopyTo(keys, 0);
                for (; _lastCleanIndex < lastIndex; _lastCleanIndex++)
                {
                    var temp = (GreyListEntry) this[_lastCleanIndex];
                    if (temp.Confirmed)
                    {
                        // Test against the confirmed timeout
                        if (now.Subtract(temp.FirstSeen) > confirmedMaxAge)
                        {
                            indexesToClean.Add((string) keys[_lastCleanIndex]);
                        }
                        continue;
                    }
                    // Test against the unconfirmed timeout
                    if (now.Subtract(temp.FirstSeen) > unconfirmedMaxAge)
                    {
                        indexesToClean.Add((string) keys[_lastCleanIndex]);
                    }
                }
                foreach (string index in indexesToClean)
                {
                    Remove(index);
                }
            }
        }
    }
}