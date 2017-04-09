using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace LeaguePredict
{
    static class GetRawGames
    {
        static string apiKey = "RGAPI-c99bc9fd-b46d-4fd6-80cf-2f334376d691";

        /// <summary>
        /// Returns the recent games data from Riot
        /// </summary>
        /// <param name="playerID">The ID of the player who's recent games to return</param>
        /// <returns>Raw Recent Game Data</returns>
        public static string GetRecentGames(string playerID)
        {
            string getstr = "https://na.api.riotgames.com/api/lol/NA/v1.3/game/by-summoner/" + playerID + "/recent?api_key=" + apiKey;
            WebClient wc = new WebClient();
            return wc.DownloadString(getstr);
        }

        /// <summary>
        /// Loops through Riot API and gets >30lvl ID's
        /// </summary>
        /// <returns>ID's of people >lvl30</returns>
        public static List<string> GetUserIDs(int min = 57067600, int max = 57467600)
        {
            List<string> eligUsers = new List<string>();
            WebClient wc = new WebClient();
            string testIds = "";
            for (int i = min; i < max; i++)
            {

                testIds += i + ",";
                if ((i - min) % 40 != 0 || i == min) continue;

                testIds = testIds.TrimEnd(',');
                string getstr = "https://na.api.riotgames.com/api/lol/NA/v1.4/summoner/" + testIds + "?api_key=" + apiKey;
                string rawdata = "";
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
                List<string> IDs = new List<string>();
                List<int> levels = new List<int>();
                levels = Util.GetMiddleAL(rawdata, "\"summonerLevel\":", "}").ConvertAll(int.Parse);
                IDs = Util.GetMiddleAL(rawdata, "\"id\":", ",");
                for (int j = 0; j < levels.Count; j++)
                    if (levels[j] == 30)
                        eligUsers.Add(IDs[j]);
                testIds = "";
            }
            return eligUsers;
        }
    }
}
