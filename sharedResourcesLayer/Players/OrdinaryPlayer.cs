using sharedResourcesLayer.Players;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sharedResourcesLayer
{
    public class OrdinaryPlayer
    {
        [Key]
        public string name { get; set; }
        public int points { get; set; }

        [NotMapped]
        public Hand hand { get; set; }
        //betting info
        public int balance { get; set; }
        public virtual Stats playerStats { get; set; }

        public OrdinaryPlayer()
        {

        }

        public OrdinaryPlayer(String name)
        {
            this.name = name;
            points = 0;
            hand = new Hand();
            balance = 0;
            playerStats = new Stats(this);
        }

        public OrdinaryPlayer(String name, int balance, Stats oldStats)
        {
            this.name = name;
            points = 0;
            hand = new Hand();
            this.balance = balance;
            playerStats = oldStats;
        }

        public void setStats(Stats newStats)
        {
            playerStats = newStats;
        }

        public Stats getStats()
        {
            return playerStats;
        }


        public void setBalance(int newBalance)
        {
            balance = newBalance;
        }

        public int getBalance()
        {
            return balance;
        }


        public void setPoints(int newTotal)
        {
            points = newTotal;
        }

        public int getPoints()
        {
            return points;
        }

        public void setName(String newName)
        {
            name = newName;
        }

        public String getName()
        {
            return name;
        }

        public Hand getHand()
        {
            return hand;
        }

        public void addCard(Card card)
        {
            hand.addCard(card);
        }

    }
}
