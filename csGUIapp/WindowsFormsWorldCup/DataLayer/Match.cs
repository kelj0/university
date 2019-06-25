using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Match
    {
        public string location { get; set; }
        public string attendance { get; set; }
        public string home_team { get; set; }
        public string away_team { get; set; }
        public string winner { get; set; }
        public string score { get; set; }
        public string id { get; set; }
        public Dictionary<string, Player> players = new Dictionary<string, Player>();
    }
}
