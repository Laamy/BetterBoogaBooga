#region Imports

using BetterBoogaBoogaLauncher.SDK;
using BetterBoogaBoogaLauncher.SDK.Structs;
using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        public class RobloxProcess
        {
            public static Process roblox;

            public class User32
            {
                [DllImport("User32.dll", SetLastError = true)]
                public static extern bool GetWindowRect(IntPtr hWnd, out ProcessRectangle lpRect);

                [DllImport("User32.dll", SetLastError = true)]
                public static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);
            }
        }

        public static void ReplaceRoblox()
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey("roblox-player\\shell\\open\\command", true);
            key.SetValue(string.Empty, "\"" + AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName + "\" %1");
            key.Close();
        }

        static void Main(string[] args)
        {
            if (args.Length == 0 && new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                ReplaceRoblox();
                MessageBox.Show("Installed launcher, you many now close this window.", "BetterBoogaBooga");
            }
            else
            {
                Task.Factory.StartNew(() => Application.Run(new LauncherWindow()));

                la = Launcher.ParseArgs(args[0]);

                var robloxVersions = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions");

                Task.Factory.StartNew(() => {
                    RobloxProcess.roblox = Process.Start(robloxVersions[robloxVersions.Length - 1] +
                    "\\RobloxPlayerBeta.exe",
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
