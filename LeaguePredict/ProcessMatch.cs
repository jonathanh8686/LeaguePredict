using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace LeaguePredict
{
    /// <summary>
    /// A structure used to store information about each player in a game
    /// </summary>
    struct PlayerStruct
    {
        public string PlayerId;
        public int ChampId;
        public string Team;
    }

    static class ProcessMatch
    {
        /// <summary>
        /// Process each of the matches
        /// </summary>
        /// <param name="matchData">The raw data as given by the Riot API</param>
        /// <returns>A list of LeagueMatches which each of the properties</returns>
        public static List<LeagueMatch> ProcessMatches(string matchData)
        {
            List<LeagueMatch> rtnMatches = new List<LeagueMatch>();
            JObject matchJson = JObject.Parse(matchData);
            var gamesData = matchJson["games"];
            // Loops through each of the games and assigns one at a time
            for (int i = 0; i < gamesData.Count(); i++)
            {
                var gameData = gamesData[i];

                // All of data about the players is stored
                var playerData = gameData["fellowPlayers"];
                List<PlayerStruct> playersIg = new List<PlayerStruct>();

                for (int j = 0; j < playerData.Count(); j++)
                {
                    // Put all players into playersIg using the PlayerStruct
                    PlayerStruct pcp = new PlayerStruct
                    {
                        ChampId = (int) playerData["championId"],
                        PlayerId = playerData["summonerId"].ToString(),
                        Team = playerData["teamId"].ToString() == "100" ? "blue" : "red"
                    };
                    playersIg.Add(pcp);
                }

                LeagueMatch tmpMatch = new LeagueMatch
                {
                    GameId = gameData["gameId"].ToString(),
                    StartDate = (long) gameData["createDate"],
                    GameType = gameData["subType"].ToString(),
                    PlayerChampions = playersIg
                    
                };

                rtnMatches.Add(tmpMatch);
            }
            return rtnMatches;
        }
    }
}
