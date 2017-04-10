using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace LeaguePredict
{
    /// <summary>
    ///     A structure used to store information about each player in a game
    /// </summary>
    internal struct PlayerStruct
    {
        public string PlayerId;
        public int ChampId;
    }

    internal static class ProcessMatch
    {
        /// <summary>
        ///     Process each of the matches
        /// </summary>
        /// <param name="matchData">The raw data as given by the Riot API</param>
        /// <returns>A list of LeagueMatches which each of the properties</returns>
        public static List<LeagueMatch> ProcessMatches(string matchData)
        {
            var rtnMatches = new List<LeagueMatch>();
            var matchJson = JObject.Parse(matchData);
            var gamesData = matchJson["games"];
            // Loops through each of the games and assigns one at a time
            for (int i = 0; i < gamesData.Count(); i++)
            {
                var gameData = gamesData[i];

                // All of data about the players is stored (except for the original)
                var playerData = gameData["fellowPlayers"];
                var redTeam = new List<PlayerStruct>();
                var blueTeam = new List<PlayerStruct>();

                for (int j = 0; j < playerData.Count(); j++)
                {
                    // Put all players into Blue/Red teams using the PlayerStruct
                    var pcp = new PlayerStruct
                    {
                        ChampId = (int) playerData[j]["championId"],
                        PlayerId = playerData[j]["summonerId"].ToString()
                    };

                    if (playerData[j]["teamId"].ToString() == "100")
                        blueTeam.Add(pcp);
                    else if (playerData[j]["teamId"].ToString() == "200")
                        redTeam.Add(pcp);
                }

                // Add the original player back, "fellowPlayers" does not contain it
                var oripcp = new PlayerStruct
                {
                    ChampId = (int) gameData["championId"],
                    PlayerId = matchJson["summonerId"].ToString()
                };

                if (gameData["teamId"].ToString() == "100")
                    blueTeam.Add(oripcp);
                else if (gameData["teamId"].ToString() == "200")
                    redTeam.Add(oripcp);

                var tmpMatch = new LeagueMatch
                {
                    GameId = gameData["gameId"].ToString(),
                    StartDate = (long) gameData["createDate"],
                    GameType = gameData["subType"].ToString(),
                    BlueTeam = blueTeam,
                    RedTeam = redTeam
                };

                rtnMatches.Add(tmpMatch);
            }
            return rtnMatches;
        }
    }
}
