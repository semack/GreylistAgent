namespace GreyListAgent
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    [XmlRoot("GreyListDatabase")]
    public class GreyListDatabase : OrderedDictionary, IXmlSerializable
    {
        /// <summary>
        /// Last index that was cleaned
        /// </summary>
        private int lastCleanIndex = 0;

        /// <summary>
        /// Loads
        /// </summary>
        /// <param name="path"></param>
        /// <returns>loaded database or empty database if it can't load the database</returns>
        public static GreyListDatabase Load(String path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GreyListDatabase));
            GreyListDatabase db;
            try
            {
                using (StreamReader source = new StreamReader(File.OpenRead(path)))
                {
                    db = (GreyListDatabase)serializer.Deserialize(source);
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
        /// Saves the database to specified path
        /// </summary>
        /// <param name="path">Path to save the database to</param>
        public void Save(String path)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GreyListDatabase));

                using (StreamWriter outputStream = new StreamWriter(File.Open(path, FileMode.Create)))
                {
                    lock (((IOrderedDictionary)this).SyncRoot)
                    {
                        serializer.Serialize(outputStream, this);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.ToString());
                return;
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e.ToString());
                return;
            }
            catch (IOException e)
            {
                Debug.WriteLine(e.ToString());
                return;
            }
            catch (SerializationException e)
            {
                Debug.WriteLine(e.ToString());
                return;
            }
        }

        /// <summary>
        /// Cleans the database. Examines CleanRowCount rows at a time. Removes unconfirmed entries older than
        /// UnconfirmedMaxAge and removes confirmed entries older than ConfirmedMaxAge
        /// </summary>
        /// <param name="CleanRowCount">Number of rows to clean per call</param>
        /// <param name="ConfirmedMaxAge">Maximum age of confirmed entries</param>
        /// <param name="UnconfirmedMaxAge">Maximum age of uconfirmed entries</param>
        public void Clean(int CleanRowCount, TimeSpan ConfirmedMaxAge, TimeSpan UnconfirmedMaxAge)
        {
            lock (((IOrderedDictionary)this).SyncRoot)
            {

                // If we don't have any items just return
                if (this.Count < 1)
                {
                    return;
                }

                // If we are at the end of the list, or are at greater than the end of the list, wrap around
                if (this.lastCleanIndex >= this.Count)
                {
                    this.lastCleanIndex = 0;
                }

                // If we are trying to clean more items than we have, adjust so we clean what we do have
                if (CleanRowCount + this.lastCleanIndex > this.Count)
                {
                    CleanRowCount = this.Count - this.lastCleanIndex;
                }
                int lastIndex = this.lastCleanIndex + CleanRowCount;
                if (lastIndex > this.Count)
                {
                    lastIndex = this.Count;
                }
                // Loop through the section of ourselves, get a list of keys to remove, then remove them
                GreyListEntry temp;
                List<string> indexesToClean = new List<string>();
                DateTime now = DateTime.UtcNow;
                object[] keys = new object[this.Keys.Count];
                this.Keys.CopyTo(keys, 0);
                for(; this.lastCleanIndex < lastIndex; this.lastCleanIndex++)
                {
                    temp = (GreyListEntry)this[this.lastCleanIndex];
                    if(temp.Confirmed)
                    {
                        // Test against the confirmed timeout
                        if (now.Subtract(temp.FirstSeen) > ConfirmedMaxAge)
                        {
                            indexesToClean.Add((String)keys[this.lastCleanIndex]);
                        }
                        continue;
                    }
                    // Test against the unconfirmed timeout
                    if (now.Subtract(temp.FirstSeen) > UnconfirmedMaxAge)
                    {
                        indexesToClean.Add((String)keys[this.lastCleanIndex]);
                    }
                }
                for (int i = 0; i < indexesToClean.Count; i++)
                {
                    this.Remove(indexesToClean[i]);
                }

            }

        }

        /// <summary>
        /// Schema overload
        /// </summary>
        /// <returns>Null</returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// XMLReader overload for reading saved serialized XML files
        /// </summary>
        /// <param name="reader">XMLReader provided for deserializing</param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(String));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(GreyListEntry));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
                return;
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("entry");
                reader.ReadStartElement("hash");
                String key = (String)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                GreyListEntry value = (GreyListEntry)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                this.Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        /// <summary>
        /// XMLWriter overload for writing serialized objects
        /// </summary>
        /// <param name="writer">XMLWriter for serializing objects</param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(String));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(GreyListEntry));
            foreach (String key in this.Keys)
            {
                writer.WriteStartElement("entry");
                writer.WriteStartElement("hash");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                GreyListEntry value = (GreyListEntry)this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
    }
}