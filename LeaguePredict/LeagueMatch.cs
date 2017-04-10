using System.Collections.Generic;

namespace LeaguePredict
{
    /// <summary>
    /// An overarching class used to store all the data in a league game
    /// TODO: Adding more stuff to this. (Avg Health, Avg AD, Avg AP)
    /// TODO: Specifying the mastery points on a champion for each player
    /// </summary>
    class LeagueMatch
    {
        public string GameId { get; set; }
        public long StartDate { get; set; }
        public string GameType { get; set; }

        public List<PlayerStruct> RedTeam { get; set; }
        public List<PlayerStruct> BlueTeam { get; set; }

        public int RedMMR { get; set; }
        public int BlueMMR { get; set; }

        public double AvgBaseAD { get; set; }
        public double AvgBaseAP { get; set; }
    }
}
