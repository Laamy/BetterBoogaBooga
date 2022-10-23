namespace BetterBoogaBoogaLauncher
{
    public class LauncherArgs
    {
        public string GameInfo, PlaceLauncherUrl, RobloxLocale, GameLocale;
        public ulong LaunchTime, TrackerId;
    }

    class Launcher
    {
        public static LauncherArgs ParseArgs(string input)
        {
            LauncherArgs output = new LauncherArgs();

            string[] args = input.Split('+');

            foreach (string arg in args)
            {
                var argTokens = arg.Split(':');
                switch (argTokens[0])
                {
                    case "gameinfo":
                        output.GameInfo = argTokens[1];
                        break;

                    case "launchtime":
                        output.LaunchTime = ulong.Parse(argTokens[1]);
                        break;

                    case "placelauncherurl":
                        output.PlaceLauncherUrl = argTokens[1];
                        break;

                    case "browsertrackerid":
                        output.TrackerId = ulong.Parse(argTokens[1]);
                        break;

                    case "robloxLocale":
                        output.RobloxLocale = argTokens[1];
                        break;

                    case "gameLocale":
                        output.GameLocale = argTokens[1];
                        break;
                }
            }

            return output;
        }
    }
}
