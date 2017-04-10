using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LeaguePredict
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            List<string> userIDs = File.ReadAllLines("eligIDs.data").ToList();
            string rawgamedata = GetRawGames.GetRecentGames(userIDs[120]);
            ProcessMatch.ProcessMatches(rawgamedata);
        }
    }
}
