using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using sharedResourcesLayer;
using sharedResourcesLayer.Players;
using System.Data.Entity.Migrations;

namespace BlackJackDAL
{
    public class BlackJackDB 
    {

        public BlackJackDB() : base()
        {
            
        }

        public List<showDbResult> selectPlayers()
        {
            using (var context = new blackjackContext2())
            {
                List<showDbResult> results = new List<showDbResult>();

                var query = (from player in context.players
                            join stats in context.playerStats
                            on player.name equals stats.name
                            select new { a = player, b = stats.wins });


                showDbResult viewObject;

                foreach(var item in query)
                {
                    viewObject = new showDbResult(item.a.name, item.a.points, item.a.balance, item.b);
                    results.Add(viewObject);
                }

                return results;
            }
        }

        public List<showDbResult> searchForPlayers(string seachString)
        {
            using (var context = new blackjackContext2())
            {
                List<showDbResult> results = new List<showDbResult>();

                var query = (from player in context.players
                             join stats in context.playerStats
                             on player.name equals stats.name
                             where player.name.Equals(seachString)
                             select new { a = player, b = stats.wins });


                showDbResult viewObject;

                foreach (var item in query)
                {
                    viewObject = new showDbResult(item.a.name, item.a.points, item.a.balance, item.b);
                    results.Add(viewObject);
                }

                return results;
            }
        }

        public List<OrdinaryPlayer> storedPlayers()
        {
            List<OrdinaryPlayer> players = new List<OrdinaryPlayer>();

            using (var db = new blackjackContext2())
            {
                var query = from player in db.players
                            select player;

                foreach(OrdinaryPlayer player in query)
                {
                    players.Add(player);
                }

            }
            
            return players;
        }

        public OrdinaryPlayer getPlayer(string name)
        {
            OrdinaryPlayer temp;

            using(var db = new blackjackContext2())
            {

                temp = db.players.Find(name);
                if(temp != null)
                {
                    temp.playerStats = db.playerStats.Find(name);
                }
            }

            return temp;
        }


        public void deletePlayer(string name)
        {
            using(var db = new blackjackContext2())
            {
                OrdinaryPlayer player = db.players.Find(name);
                Stats playerStats = db.playerStats.Find(name);
                if(player != null && playerStats != null)
                {
                    db.playerStats.Remove(playerStats);
                    db.players.Remove(player);
                    Console.WriteLine("Player: " + name + " deleted");
                    db.SaveChanges();
                } else
                {
                    Console.WriteLine("Player with name: " + name + " does not exist");
                }
            }  
        }

        public void updateAddPlayer(OrdinaryPlayer addUpdate)
        {
            using (var db = new blackjackContext2())
            {
                db.players.AddOrUpdate(addUpdate);
                db.playerStats.AddOrUpdate(addUpdate.playerStats);
                db.SaveChanges();
            }
        }

        public void updateAddPlayers(List<OrdinaryPlayer> players)
        {
            using(var db = new blackjackContext2())
            {
                
                for(int i = 0; i < players.Count; i++)
                {
                    db.players.AddOrUpdate(players[i]);
                    db.playerStats.AddOrUpdate(players[i].playerStats);
                }

                db.SaveChanges();
            }
        }
   }

    public class blackjackContext2 : DbContext
    {
        public DbSet<OrdinaryPlayer> players { get; set; }
        public DbSet<Stats> playerStats { get; set; }
    }
}
