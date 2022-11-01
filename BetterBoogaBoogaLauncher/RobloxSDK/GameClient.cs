using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;

namespace BetterBoogaBoogaLauncher.RobloxSDK
{
    class GameClient
    {
        private static WebClient wc = new WebClient();

        public static RobloxUniverse GetMainUniverse(string id) // gonna develop a server so i dont have to wait like this every time i wanna call an api
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            RobloxPlace place = jss.Deserialize<RobloxPlace>(
                wc.DownloadString("https://api.roblox.com/universes/get-universe-containing-place?placeid=" + id));

            string json = wc.DownloadString("https://games.roblox.com/v1/games?universeIds=" + place.UniverseId);

            return jss.Deserialize<RobloxUniverse>(json);
        }
    }

    public class RobloxPlace
    {
        public long UniverseId { get; set; }
    }

    public class Creator
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public bool isRNVAccount { get; set; }
        public bool hasVerifiedBadge { get; set; }
    }

    public class Datum
    {
        public long id { get; set; }
        public long rootPlaceId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string sourceName { get; set; }
        public string sourceDescription { get; set; }
        public Creator creator { get; set; }
        public object price { get; set; }
        public List<string> allowedGearGenres { get; set; }
        public List<object> allowedGearCategories { get; set; }
        public bool isGenreEnforced { get; set; }
        public bool copyingAllowed { get; set; }
        public int playing { get; set; }
        public int visits { get; set; }
        public int maxPlayers { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public bool studioAccessToApisAllowed { get; set; }
        public bool createVipServersAllowed { get; set; }
        public string universeAvatarType { get; set; }
        public string genre { get; set; }
        public bool isAllGenre { get; set; }
        public bool isFavoritedByUser { get; set; }
        public int favoritedCount { get; set; }
    }

    public class RobloxUniverse
    {
        public List<Datum> data { get; set; }
    }
}
