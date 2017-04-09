using System.Collections.Generic;
using System.Net;

namespace LeaguePredict
{
    static class GetRawGames
    {
        /// <summary>
        /// Loops through Riot API and gets >30lvl ID's
        /// </summary>
        /// <returns>ID's of people >lvl30</returns>
        private static List<string> GetUserIDs(int min = 50000000, int max = 57467600)
        {
            string apiKey = "";
            List<string> eligUsers = new List<string>();
            WebClient wc = new WebClient();
            string testIDS = "";
            for (int i = min; i < max; i++)
            {
                testIDS = testIDS.TrimEnd(',');
                testIDS += i + ",";
                if ((i - min) % 40 != 0) continue;

                string getstr = "https://na.api.riotgames.com/api/lol/NA/v1.4/summoner/" + testIDS + "?api_key=" + apiKey;
                string rawdata = wc.DownloadString(getstr);
            }
            return eligUsers;
        }
    }
}
