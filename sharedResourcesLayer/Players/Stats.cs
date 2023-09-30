using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sharedResourcesLayer.Players
{
    public class Stats
    {
        [Key]
        [ForeignKey("player")]
        public string name { get; set; }
        public int wins { get; set; }

        public OrdinaryPlayer player { get; set; }

        public Stats()
        {

        }

        public Stats(OrdinaryPlayer player)
        {
            wins = 0;
            this.name = player.getName();
            this.player = player;
        }

        public Stats(int oldWins, OrdinaryPlayer player)
        {
            wins = oldWins;
            name = player.getName();
            this.player = player;
        }

        public void setPlayer(OrdinaryPlayer player)
        {
            this.player = player;
        }

        public OrdinaryPlayer getPlayer()
        {
            return player;
        }

        public void setWins(int newTotalOfWins)
        {
            wins = newTotalOfWins;
        }

        public int getWins()
        {
            return wins;
        }

    }
}
