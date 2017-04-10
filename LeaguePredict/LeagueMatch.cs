using System.Collections.Generic;

namespace LeaguePredict
{
    /// <summary>
    /// An overarching class used to store all the data in a league game
    /// TODO: Adding more stuff to this. (Avg Health, Avg AD, Avg AP)
    /// </summary>
    class LeagueMatch
    {
        public string GameId { get; set; }
        public long StartDate { get; set; }
        public string GameType { get; set; }

        public List<PlayerStruct> PlayerChampions { get; set; }

    }
}
