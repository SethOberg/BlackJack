using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using sharedResourcesLayer;
using GameCardLib;
using sharedResourcesLayer.Cards;
using sharedResourcesLayer.Players;

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game gameHandler;
        NewGame newGameWindow;
        DatabaseView databaseWindow;

        public MainWindow()
        {
            InitializeComponent();
            newGameWindow = new NewGame(this);
            newGameWindow.Hide();

            gameHandler = new Game();

            databaseWindow = new DatabaseView(this, gameHandler);
            databaseWindow.Hide();

           
            gameButtonsEnabled(false);

            gameHandler.playerPickedCard += updateInfoBlock;
            gameHandler.nextPlayerTurn += updateInfoBlock;
            gameHandler.shuffleDeck += updateInfoBlock;
        }

        private void hitBtn_Click(object sender, RoutedEventArgs e)
        {
            gameHandler.giveCardToActivePlayer();
            updatePlayerView(gameHandler.getActivePlayerObject());
        }

        public void updateInfoBlock(object sender, GameEventInfo info)
        {
            gameInfoBlock.Text = info.info;

            if(info.gameState == false)
            {
                gameButtonsEnabled(false);
            }

            updateRoundsLeft();
        }



        private void standBtn_Click(object sender, RoutedEventArgs e)
        {
            gameHandler.switchPlayer();
            updatePlayerView(gameHandler.getActivePlayerObject());
            updateComputerView(gameHandler.GetComputer());
        }

        public void gameButtonsEnabled(bool state)
        {
            standBtn.IsEnabled = state;
            hitBtn.IsEnabled = state;
            shuffleBtn.IsEnabled = state;
        }


        private void shuffleBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult shuffle = MessageBox.Show("Shuffle deck?", "Warning", MessageBoxButton.YesNo);

            switch (shuffle)
            {
                case MessageBoxResult.Yes:

                    gameHandler.shuffle();
                   
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void newGameBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult newGame = MessageBox.Show("Start new game?", "Warning", MessageBoxButton.YesNo);

            switch (newGame)
            {
                case MessageBoxResult.Yes:
                    //Open new NewGame window, retrieve information from window
                    newGameWindow.Show();
                    this.Hide();
                    break;
                case MessageBoxResult.No:
                    //Do nothing
                    break;
            }
        }

        private void quitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult close = MessageBox.Show("Quit game?", "Warning", MessageBoxButton.YesNo);

            switch(close) {
                case MessageBoxResult.Yes:
                    System.Windows.Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }


        public void startNewgame(int players, int decks, int rounds)
        {
            gameHandler.newGame(players, decks, rounds);
            gameInfoBlock.Text = "New Game Started!" + "\nCurrent player: " + gameHandler.activePlayer();
            totalPlayers.Text = players.ToString();
            totalDecks.Text = decks.ToString();
            updateRoundsLeft();
            updateCardsLeft();
            clearPlayerAndComputerView();
            displayPlayerImages(gameHandler.getActivePlayerObject());
           
            this.Show();
            newGameWindow.Hide();
            gameButtonsEnabled(true);
        }

        public void clearPlayerAndComputerView()
        {
            clearComputerView();
            clearPlayerView();
        }

        public void clearComputerView()
        {
            displayComputerPlayerInfo();
            displayPlayerImages(gameHandler.GetComputer());
        }

        public void clearPlayerView()
        {
            player1List.ItemsSource = null;
            displayCurrentPlayerInfo();
        }

        public void gameOver()
        {
            gameHandler.setGameActive(false);
        }


        public void updatePlayerView(OrdinaryPlayer player)
        {
            displayCurrentPlayerInfo();
            displayPlayerImages(player);
        }

        public void updateComputerView(OrdinaryPlayer player)
        {
            displayComputerPlayerInfo();
            displayPlayerImages(player);
        }


        public void displayPlayerImages(OrdinaryPlayer player)
        {
            List<string> imagePaths = player.getHand().getCardImages();
            
            //Create a list of CardImage objects
            //To bind to the databinding in the list in the MainWindow GUI
            List<CardImage> images = new List<CardImage>();

            if(imagePaths.Count > 0)
            {
                CardImage temp;
                for (int i = 0; i < imagePaths.Count; i++)
                {
                    temp = new CardImage();
                    temp.imagePath = imagePaths[i];
                    images.Add(temp);
                }
            }

            if(player.getName().Equals("dealer"))
            {
                player2List.ItemsSource = images;
            } else
            {
                player1List.ItemsSource = images;
            }
        }

        
        public void updateCardsLeft()
        {
            //Lambda statement
            Action<string> cardsLeftText = resText =>
            {
                cardsLeft.Text = resText;
            };
            cardsLeftText(gameHandler.getAvailablecards().ToString());
        }

        public void updateRoundsLeft()
        {
            roundsLeft.Text = gameHandler.getRoundsLeft().ToString();
        }

        public void displayCurrentPlayerInfo()
        {
            OrdinaryPlayer temp =  gameHandler.getActivePlayerObject();
            player1Name.Text = temp.getName();
            player1CardCount.Text = temp.getHand().getCardCount().ToString();
            player1HandValue.Text = temp.getHand().getHandValue().ToString();
            player1Points.Text = temp.getPoints().ToString();
        }

        public void displayComputerPlayerInfo()
        {
            ComputerPlayer dealer = gameHandler.GetComputer();
            player2Name.Text = dealer.getName();
            player2CardCount.Text = dealer.getHand().getCardCount().ToString();
            player2HandValue.Text = "?";
            player2Points.Text = dealer.getPoints().ToString();
        }

        public void displayComputerHandValue()
        {
            player2HandValue.Text = gameHandler.GetComputer().getHand().getHandValue().ToString();
        }

        private void databaseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            databaseWindow.Show();
        }
    }
}
