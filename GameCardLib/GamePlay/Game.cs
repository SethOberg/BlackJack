using sharedResourcesLayer.Players;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using sharedResourcesLayer;
using BlackJackDAL;
using GameCardLib.GamePlay;

namespace GameCardLib
{
    public class Game
    {
        private List<OrdinaryPlayer> players;
        private ComputerPlayer dealer;
        private Deck deck;
        private int activePlayerIndex = 0;
        private bool activeGame = false;
        private int totalRounds;
        private int roundsLeft;
        private BlackJackDB database;

        //Event delegates
        public event EventHandler<GameEventInfo> playerPickedCard;
        public event EventHandler<GameEventInfo> nextPlayerTurn;
        public event EventHandler<GameEventInfo> shuffleDeck;

        public event EventHandler<DatabaseInfo> updateDataGrid;
        public event EventHandler<DatabaseInfo> updateSearchGrid;

        public Game()
        {
            players = new List<OrdinaryPlayer>();
            deck = new Deck();
            totalRounds = 0;
            roundsLeft = 0;
            database = new BlackJackDB();
            database.deletePlayer("Hej");
        }

        //Methods used to notify subscribers to Event delegates 
        public void notifyShuffleEvent(string info)
        {
            if (shuffleDeck != null)
            {
                shuffleDeck(this, new GameEventInfo(info, activeGame));
            }
        }

        public void notifyStandEvent(string info)
        {
            if (nextPlayerTurn != null)
            {
                nextPlayerTurn(this, new GameEventInfo(info, activeGame));
            }
        }

        public void notifyPlayerHitEvent(string info)
        {
            if (playerPickedCard != null)
            {
                playerPickedCard(this, new GameEventInfo(info, activeGame));
            }
        }

        public void notifyDataGrid()
        {
            if (updateDataGrid != null)
            {
                updateDataGrid(this, new DatabaseInfo(getDatabaseEntries()));
            }
        }

        public void notifySearchDataGrid(string search)
        {
            if (updateSearchGrid != null)
            {
                updateSearchGrid(this, new DatabaseInfo(getSearchResults(search)));
            }
        }

        public void sendDataToDatabaseView()
        {
            notifyDataGrid();
        }

        public void reset()
        {
            players = new List<OrdinaryPlayer>();
            deck = new Deck();
        }

        public void resetPoints()
        {
            for(int i = 0; i < players.Count; i++)
            {
                players[i].setPoints(0);
            }
        }

        public void addDeck(int amount)
        {
            deck.addDecks(amount);
            shuffle();
        }

        public OrdinaryPlayer getActivePlayerObject()
        {
            return players[activePlayerIndex];
        }

        public void addPlayer(int amount)
        {
            string playerName;
            for(int i = 0; i < amount; i++)
            {
                playerName = "Player" + (i + 1);
                players.Add(new OrdinaryPlayer(playerName));
            }

            matchPlayersToDatabase(players);
        }

        public void switchPlayer()
        {
            String res;
            if((activePlayerIndex + 1) >= players.Count)
            {
                activePlayerIndex = 0;
                res = newRound();
            } else
            {
                String temp = players[activePlayerIndex].getName() + " did not pick up a new card \n";
                activePlayerIndex += 1;
                res = temp + "Turn: " + players[activePlayerIndex].getName();
            }

            notifyStandEvent(res);
        }

        public String newRound()
        {
            String res; 
            if(roundsLeft == 1)
            {
                determineRoundWin();
                res = finalWin();
            } else
            {
                res = determineRoundWin();
            }
            return res;
        }

        public String determineRoundWin()
        {
            OrdinaryPlayer winningPlayer = players[0];
            int winningScore = 0;
            int tempScore;
            String res;
            bool onePlayerWon = false;

            computerTurn();


            for(int i = 0; i < players.Count; i++)
            {
                tempScore = players[i].getHand().getHandValue();
                if(tempScore <= 21)
                {
                    if(winningScore < tempScore)
                    {
                        winningScore = tempScore;
                        winningPlayer = players[i];
                        onePlayerWon = true;
                    }
                }
            }

            //dealer left, compare with winning player
            tempScore = dealer.getHand().getHandValue();

            if(tempScore <= 21)
            {
                if(winningScore < tempScore)
                {
                    winningScore = tempScore;
                    winningPlayer = dealer;
                }
            }

            String temp = "Turn: " + players[activePlayerIndex].getName();

            if(onePlayerWon)
            {
                int currentPoints = winningPlayer.getPoints();
                winningPlayer.setPoints((currentPoints += 1));

                //increment players wins, later saved to database
                //Stats winningPlayerStats = winningPlayer.getStats();
                //winningPlayerStats.wins += 1;

                res = winningPlayer.getName() + " won the round with a hand value of " + winningPlayer.getHand().getHandValue() + " !\n" + temp;

            } else
            {
                res = "No player won this round";
            }

            if(roundsLeft > 1)
            {
                clearPlayerCards();
            }
            
            roundsLeft -= 1;
            return res;
        }


        public String finalWin()
        {
            string res;
            OrdinaryPlayer winningPlayer = players[0];
            List<string> winnerNames = new List<string>();

            for(int i = 0; i < players.Count; i++)
            {
                if(players[i].getPoints() > winningPlayer.getPoints())
                {
                    winningPlayer = players[i];
                    winnerNames.Clear();
                    winnerNames.Add(players[i].getName());
                } 
                else if(players[i].getPoints() == winningPlayer.getPoints())
                {
                    winnerNames.Add(players[i].getName());
                }
            }

            //compare with computer
            if(dealer.getPoints() > winningPlayer.getPoints())
            {
                winningPlayer = dealer;
                winnerNames.Clear();
                winnerNames.Add(dealer.getName());
            } else if(dealer.getPoints() == winningPlayer.getPoints())
            {
                winnerNames.Add(dealer.getName());
            }

            res = winnerInfo(winnerNames, winningPlayer.getPoints());
            activeGame = false;

            Stats winningPlayerStats = winningPlayer.getStats();
            winningPlayerStats.wins += 1;

            //after finished game, save players to database again
            //if player already exists their info will be updated
            //otherwise a new player object will be written to the database
            resetPoints();
            database.updateAddPlayers(players);

            return res;
        }


        public void deletePlayerFromDB(string name)
        {
            if(database != null)
            {
                database.deletePlayer(name);
            }
        }

        public void searchForPlayers(string searchString)
        {
            if (database != null)
            {
                //database.searchForPlayers(searchString);
                notifySearchDataGrid(searchString);
            }
        }


        public String winnerInfo(List<string> winners, int points)
        {
            String res;
            String players = "";

            if(winners.Count > 1)
            {

                for(int i = 0; i < winners.Count; i++)
                {
                    if(i == (winners.Count - 1))
                    {
                        players += " and " + winners[i];
                    } else
                    {
                        players += winners[i] + ", ";
                    }
                }

                res = "Tie between " + players;

            } else
            {
                res = winners[0] + " won the game with " + points + " points!";
            }

            if(points == 0)
            {
                res = "No one won the game, everyone scored 0 points";
            }

            return res;
        }


        public void clearPlayerCards()
        {
            for(int i = 0; i < players.Count; i++)
            {
                players[i].getHand().clearHand();
            }

            dealer.getHand().clearHand();
            initializePlayers();
            initializeDealer();
        }

        public void computerTurn()
        {
            //Make all dealers cards face up
            
            while(dealer.takeCard())
            {
                dealer.addCard(deck.getCard());
            }

            dealer.showAllCards();
        }

        public List<showDbResult> getDatabaseEntries()
        {
            return database.selectPlayers();
        }

        public List<showDbResult> getSearchResults(string search)
        {
            return database.searchForPlayers(search);
        }

        public void giveCardToActivePlayer()
        {
            String temp = activePlayer() + " picked up a new card \n";
            temp += "Turn: " + activePlayer();
            players[activePlayerIndex].addCard(deck.getCard());
            notifyPlayerHitEvent(temp);
        }


        public String activePlayer()
        {
            return players[activePlayerIndex].getName();
        }

        public int getAvailablecards()
        {
            return deck.getAmountOfAvailableCards();
        }

        public void stand()
        {
            switchPlayer();
        }

        public void hit()
        {
            players[activePlayerIndex].addCard(deck.getCard());
            switchPlayer();
        }

        public void shuffle()
        {
            string res; 

            if (gameActive())
            {
                if (getAvailablecards() > 1)
                {
                    deck.shuffleDeck();
                    res = "Deck shuffled";
                }
                else
                {
                    res = "Only 1 card left, cannot shuffle";
                }
            }
            else
            {
                res = "Cannot shuffle, start game!";
            }
            notifyShuffleEvent(res);
        }


        public void matchPlayersToDatabase(List<OrdinaryPlayer> newPlayerList)
        {
            String temp;
            OrdinaryPlayer tempPlayer;
            for(int i = 0; i < newPlayerList.Count; i++)
            {
                temp = newPlayerList[i].getName();
                tempPlayer = database.getPlayer(temp);
                if(tempPlayer != null)
                {
                    newPlayerList[i] = tempPlayer;
                    //Hand is not mapped to database
                    //Give each player new hand
                    newPlayerList[i].hand = new Hand();
                }
            }

        }


        public void newGame(int players, int decks, int rounds)
        {
            reset();
            addPlayer(players);
            addDeck(decks);
            activeGame = true;
            int computerId = players + 1;
            dealer = new ComputerPlayer("dealer", computerId);
            initializeDealer();
            initializePlayers();
            this.totalRounds = rounds;
            roundsLeft = rounds;
        }

        public int getRoundsLeft()
        {
            return roundsLeft;
        }


        public void initializePlayers()
        {
            for(int i = 0; i < players.Count; i++)
            {
                players[i].addCard(deck.getCard());
                players[i].addCard(deck.getCard());
            }
        }

        public void initializeDealer()
        {
            //Card 1, open
            Card temp = deck.getCard();
            dealer.addCard(temp);

            //Card 2, hidden
            temp = deck.getCard();
            temp.setFaceUp(false);
            dealer.addCard(temp);
        }

        public ComputerPlayer GetComputer()
        {
            return dealer;
        }

        public bool gameActive()
        {
            return activeGame;
        }

        public void setGameActive(bool active)
        {
            activeGame = active;
        }

    }
}
