using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharedResourcesLayer
{
    public class showDbResult
    {
        public string name { get; set; }
        public int points { get; set; }
        public int balance { get; set; }
        
        public int wins { get; set; }

        public showDbResult(string name, int points, int balance, int wins)
        {
            this.name = name;
            this.points = points;
            this.balance = balance;
            this.wins = wins;
        }

    }
}
