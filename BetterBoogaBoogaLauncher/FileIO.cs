using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
    namespace MDI
    {
        public class MDI // Main directory information
        {
            public static string mdiBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BetterBoogaBooga\\";
            public static bool Init = false;

            public static void Initialize()
            {
                if (Init) return;
                Init = true;

                if (!Directory.Exists(mdiBase))
                    Directory.CreateDirectory(mdiBase);
            }
        }

        public class MDIFile
        {
            /// <summary>
            /// Check if file exists then creates/writes to a new one if it doesnt
            /// </summary>
            public static void CheckWrite(string path, string data)
            {
                MDI.Initialize();

                if (!File.Exists(MDI.mdiBase + path))
                    File.WriteAllText(MDI.mdiBase + path, data);
            }

            /// <summary>
            /// Write to a file
            /// </summary>
            public static void Write(string path, string data)
            {
                MDI.Initialize();

                File.WriteAllText(MDI.mdiBase + path, data);
            }

            /// <summary>
            /// Checks if the file exists then returns either replacementData or the files context if it exists
            /// </summary>
            public static string CheckReplaceRead(string path, string replacementData = "")
            {
                string data = replacementData;

                if (File.Exists(MDI.mdiBase + path))
                    data = File.ReadAllText(MDI.mdiBase + path);

                return data;
            }
        }

        public class MDIDirectory
        {
            /// <summary>
            /// Check if folder exists if not creates a new one
            /// </summary>
            public static void CheckCreate(string path)
            {
                MDI.Initialize();

                if (!Directory.Exists(MDI.mdiBase + path))
                    Directory.CreateDirectory(MDI.mdiBase + path);
            }

            /// <summary>
            /// Checks if folder exists if so then deletes it
            /// </summary>
            public static void CheckDelete(string path)
            {
                MDI.Initialize();

                if (Directory.Exists(MDI.mdiBase + path))
                    Directory.Delete(MDI.mdiBase + path);
            }
        }


        class MDIInIFile   // revision 11
        {
            string Path;
            string EXE = Assembly.GetExecutingAssembly().GetName().Name;

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

            public MDIInIFile()
            {
                MDI.Initialize();

                Path = MDI.mdiBase + "config.ini";
            }

            public string Read(string Key, string Section = null)
            {
                var RetVal = new StringBuilder(255);
                GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
                return RetVal.ToString();
            }

            public void Write(string Key, string Value, string Section = null)
            {
                WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
            }

            public void DeleteKey(string Key, string Section = null)
            {
                Write(Key, null, Section ?? EXE);
            }

            public void DeleteSection(string Section = null)
            {
                Write(null, null, Section ?? EXE);
            }

            public bool KeyExists(string Key, string Section = null)
            {
                return Read(Key, Section).Length > 0;
            }
        }
    }
}
