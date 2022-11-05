using System.Diagnostics;
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
            Process.GetCurrentProcess().Kill();
        }
    }
}
