using System.Diagnostics;
using System.MDI;
using System.Net;
using System.Threading;

namespace BetterBoogaBoogaLauncher.RobloxSDK
{
    public class RobloxClient
    {
        public static Mutex robloxMutex;

        public static void InitMutex()
        {
            robloxMutex = new Mutex(true, "ROBLOX_singletonMutex");
        }

        public static void UninitMutex()
        {
            if (robloxMutex != null)
            {
                robloxMutex.Close();
                robloxMutex.Dispose();
            }
        }

        public static void ExitApp() // this is called everytime we want to exit the application
        {
            UninitMutex();
            Process.GetCurrentProcess().Kill();
        }

        static WebClient wc = new WebClient();
        public static void UpdateRoblox()
        {
            MDIDirectory.CheckCreate("Tmp");
            wc.DownloadFile("https://setup.rbxcdn.com/" + Program.RobloxProcess.version + "-Roblox.exe",
                MDI.mdiBase + "\\Tmp\\RobloxPlayerLauncher.exe");
            Process.Start(MDI.mdiBase + "\\Tmp\\RobloxPlayerLauncher.exe");
        }
    }
}
