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
using System.Windows.Shapes;
using UtilitiesLibrary.ConvertString;

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame : Window
    {
        private MainWindow mainWindow;

        public NewGame(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Hide();
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if (showInfo())
            {
                int players = StringToNumbers.convertToInt(playerAmount.Text);
                int decks = StringToNumbers.convertToInt(deckAmount.Text);
                int rounds = StringToNumbers.convertToInt(roundAmount.Text);
                mainWindow.startNewgame(players, decks, rounds);
                resetText();
            } 
        }

        private bool showInfo()
        {
            bool res = true;

            if(!StringToNumbers.stringToIntOk(playerAmount.Text))
            {
                playerAmountInfo.Text = "Please enter numbers";
                res = false;
            } else
            {
                if(StringToNumbers.convertToInt(playerAmount.Text) <= 0)
                {
                    playerAmountInfo.Text = "Enter number larger than 0";
                    res = false;
                } else
                {
                    playerAmountInfo.Text = "";
                }
            }

            if(!StringToNumbers.stringToIntOk(deckAmount.Text))
            {
                deckAmountInfo.Text = "Please enter numbers";
                res = false;
            } else
            {
                if (StringToNumbers.convertToInt(deckAmount.Text) <= 0)
                {
                    deckAmountInfo.Text = "Enter number larger than 0";
                    res = false;
                }
                else
                {
                    deckAmountInfo.Text = "";
                }
            }

            if(!StringToNumbers.stringToIntOk(roundAmount.Text))
            {
                roundAmountInfo.Text = "Please enter numbers";
                res = false;
            } else
            {
                if (StringToNumbers.convertToInt(roundAmount.Text) <= 0)
                {
                    roundAmountInfo.Text = "Enter number larger than 0";
                    res = false;
                }
                else
                {
                    roundAmountInfo.Text = "";
                }
            }
            return res;
        }

        private void resetText()
        {
            playerAmount.Text = "";
            deckAmount.Text = "";
            roundAmount.Text = "";
        }
    }
}
