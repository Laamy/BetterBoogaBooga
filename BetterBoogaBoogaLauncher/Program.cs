#region Imports

using BetterBoogaBoogaLauncher.RobloxSDK;

using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.MDI;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

#endregion

namespace BetterBoogaBoogaLauncher
{
    internal class Program
    {
        public static LauncherArgs la;
        public static MDIInIFile config = new MDIInIFile();

        public class RobloxProcess
        {
            public static Process roblox;
            public static RobloxUniverse curPlace;
            public static string version;

            public class User32
            {
                [DllImport("User32.dll", SetLastError = true)]
                public static extern bool GetWindowRect(IntPtr hWnd, out ProcessRectangle lpRect);

                [DllImport("User32.dll", SetLastError = true)]
                public static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);
            }
        }

        public static void ReplaceRoblox(string proc = null)
        {
            if (proc == null)
                proc = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName;

            RegistryKey key = Registry.ClassesRoot.OpenSubKey("roblox-player\\shell\\open\\command", true);
            key.SetValue(string.Empty, "\"" + proc + "\" %1");
            key.Close();
        }

        public static bool CheckAdminPerms() => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.Run(new InstallerWindow());
            }
            else
            {
                la = Launcher.ParseArgs(args[0]);

                // https://setup.rbxcdn.com/version

                string robloxFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    + "\\Roblox\\Versions";
                string robloxPFPath = "C:\\Program Files (x86)\\Roblox\\Versions"; // some people have other folder so this fixes it ig

                WebClient wc = new WebClient();

                RobloxProcess.version = wc.DownloadString("https://setup.rbxcdn.com/version");

                string robloxPath = "";

                if (!Directory.Exists(robloxFolder + "\\" + RobloxProcess.version) && !Directory.Exists(robloxPFPath + "\\" + RobloxProcess.version))
                {
                    config.Write("RequiresReinstall", "1", "System");

                    Task.Factory.StartNew(() => Application.Run(new InstallerWindow(true)));
                    Thread.Sleep(-1);
                }
                else
                {
                    if (File.Exists(MDI.mdiBase + "config.ini"))
                    {
                        if (config.KeyExists("RequiresReinstall", "System")
                            && config.Read("RequiresReinstall", "System") != "0")
                        {
                            if (!CheckAdminPerms())
                            {
                                MessageBox.Show("Roblox cant start due to needing a reinstall", "BBRB");
                                RobloxClient.ExitApp();
                            }
                        }
                    }

                    if (Directory.Exists(robloxFolder + "\\" + RobloxProcess.version))
                        robloxPath = robloxFolder + "\\" + RobloxProcess.version;

                    if (Directory.Exists(robloxPFPath + "\\" + RobloxProcess.version))
                        robloxPath = robloxPFPath + "\\" + RobloxProcess.version;
                }

                string placeId = HttpUtility.UrlDecode(la.PlaceLauncherUrl).Split('&')[2].Split('=')[1];

                RobloxProcess.curPlace = GameClient.GetMainUniverse(placeId);

                Task.Factory.StartNew(() => Application.Run(new LauncherWindow()));

                // multi instance cuz im a fucking gay fuck
                RobloxClient.InitMutex();

                Task.Factory.StartNew(() => {
                    RobloxProcess.roblox = Process.Start(robloxPath + "\\RobloxPlayerBeta.exe",
                    $"--play -a https://www.roblox.com/Login/Negotiate.ashx -t {la.GameInfo}" +
                    $" -j {HttpUtility.UrlDecode(la.PlaceLauncherUrl)} -b {la.TrackerId} --launchtime={la.LaunchTime}" +
                    $" --rloc {la.RobloxLocale} --gloc {la.GameLocale}");
                });

                Thread.Sleep(-1); // pause console
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ProcessRectangle
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
        public ProcessRectangle(Point position, Point size) // this is most likely wrong
        {
            Left = position.X;
            Top = position.X + size.X;
            Right = position.Y;
            Bottom = position.Y + size.Y;

            // Left, Top, Right, Bottom
            // X, X - X, Y, Y - Y

            // Left, Top,
            // Right, Bottom
            // X, X - X,
            // Y, Y - Y
        }
    }
}
