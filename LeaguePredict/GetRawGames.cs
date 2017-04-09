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

            List<string> eligUsers = new List<string>();
            WebClient wc = new WebClient();
            string testIDS = "";
            for (int i = min; i < max; i++)
            {
                testIDS += i + ",";
                if ((i - min) % 40 == 0)
                {
                    string getstr =
                        "https://na.api.riotgames.com/api/lol/NA/v1.4/summoner/1,2,3,4,5?api_key=RGAPI-c99bc9fd-b46d-4fd6-80cf-2f334376d691";
                    string rawdata = wc.DownloadString("");
                    testIDS = "";
                }
            }
            return eligUsers;
        }
    }
}
