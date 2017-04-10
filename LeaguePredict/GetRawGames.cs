using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace LeaguePredict
{
    static class GetRawGames
    {
        static readonly string apiKey = "";

        /// <summary>
        /// Returns the recent games data from Riot
        /// </summary>
        /// <param name="playerId">The ID of the player who's recent games to return</param>
        /// <returns>Raw Recent Game Data</returns>
        public static string GetRecentGames(string playerId)
        {
            string getstr = "https://na.api.riotgames.com/api/lol/NA/v1.3/game/by-summoner/" + playerId + "/recent?api_key=" + apiKey;
            return new WebClient().DownloadString(getstr);
        }

        /// <summary>
        /// Loops through Riot API and gets >30lvl ID's
        /// A one time / not often run
        /// </summary>
        /// <returns>ID's of people >lvl30</returns>
        public static List<string> GetUserIDs(int min = 57067600, int max = 57467600)
        {
            List<string> eligUsers = new List<string>();
            WebClient wc = new WebClient();
            string getIds = "";
            for (int i = min; i < max; i++)
            {
                getIds += i + ",";
                if ((i - min) % 40 != 0 || i == min) continue;

                getIds = getIds.TrimEnd(',');
                string getstr = "https://na.api.riotgames.com/api/lol/NA/v1.4/summoner/" + getIds + "?api_key=" + apiKey;
                string rawdata;
                try
                {
                    // Riot API limits 10req/10sec and 500req/10min
                    rawdata = wc.DownloadString(getstr);
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                var levels = Util.GetMiddleAL(rawdata, "\"summonerLevel\":", "}").ConvertAll(int.Parse);
                var ids = Util.GetMiddleAL(rawdata, "\"id\":", ",");
                for (int j = 0; j < levels.Count; j++)
                    if (levels[j] == 30)
                        eligUsers.Add(ids[j]);
                getIds = "";
            }
            return eligUsers;
        }
    }
}
