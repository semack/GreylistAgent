using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GreyListAgent.Configurator.Common
{
    // https://msdn.microsoft.com/en-us/library/hh127450(v=vs.85).aspx

    public static class ControlPanelControl
    {
        private static Guid AssemblyGuid
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                return assembly.GetType().GUID;
            }
        }

        private static string LocalMachineKey
        {
            get
            {
                var keyPath =
                    $"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ControlPanel\\NameSpace\\{"{"}{AssemblyGuid}{"}"}";
                return keyPath;
            }
        }

        private static string ClassesRootKey
        {
            get
            {
                var keyPath = $"CLSID\\{"{"}{AssemblyGuid}{"}"}";
                return keyPath;
            }
        }

        public static void Register()
        {
            Unregister();

            var key = Registry.LocalMachine.CreateSubKey(LocalMachineKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (key != null)
            {
                key.SetValue(null, "Exchange Greylist Configuration", RegistryValueKind.String);
                key.Close();
            }

            key = Registry.ClassesRoot.CreateSubKey(ClassesRootKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (key != null)
            {
                key.SetValue(null, "Exchange Greylist Configuration", RegistryValueKind.String);
                key.SetValue("InfoTip", "Configuration utility of Greylist Agent for Microsoft Exchange Server",
                    RegistryValueKind.String);
                key.SetValue("System.ApplicationName", "GreyList.Configuration", RegistryValueKind.String);
                key.SetValue("System.ControlPanel.Category", "3,8", RegistryValueKind.String);
                key.Close();

                var keyPath = $"{ClassesRootKey}\\DefaultIcon";
                var exeFullPath = $"{Application.ExecutablePath}";

                key = Registry.ClassesRoot.CreateSubKey(keyPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                if (key != null)
                {
                    key.SetValue(null, $"{exeFullPath}, {-2}", RegistryValueKind.String);
                    key.Close();

                    keyPath = $"{ClassesRootKey}\\Shell\\Open\\Command";
                }
                key = Registry.ClassesRoot.CreateSubKey(keyPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
                key?.SetValue(null, exeFullPath, RegistryValueKind.ExpandString);
                key?.Close();            
            }     
        }

        public static void Unregister()
        {
            Registry.LocalMachine.DeleteSubKey(LocalMachineKey);
            Registry.ClassesRoot.DeleteSubKeyTree(ClassesRootKey);
        }
    }
}