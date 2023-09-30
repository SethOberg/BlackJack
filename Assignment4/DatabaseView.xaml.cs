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
using GameCardLib;
using GameCardLib.GamePlay;
using sharedResourcesLayer;

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for DatabaseView.xaml
    /// </summary>
    /// 
     
    public partial class DatabaseView : Window
    {
        private MainWindow mainWindow;
        private Game game;
        public DatabaseView(MainWindow mainWindow, Game game)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.game = game;

            game.updateDataGrid += updateDB;
            game.updateSearchGrid += updateSearch;
        }


        public void deleteItemFromDB(string name)
        {
            game.deletePlayerFromDB(name);
        }

        public bool rowSelected()
        {
            bool res = false;

            if(databaseGrid.SelectedItem != null)
            {
                res = true;
                infoBlock.Text = "Item deleted";
            } else
            {
                infoBlock.Text = "Select item first";
            }

            return res;
        }


        public void updateDB(object sender, DatabaseInfo info)
        {
            databaseGrid.ItemsSource = info.savedPlayers;
            infoBlock.Text = "All results";
        }

        public void updateSearch(object sender, DatabaseInfo info)
        {
            searchGrid.ItemsSource = info.savedPlayers;
            searchInfo.Text = "All results from search: " + info.savedPlayers.Count;
        }

        public void buttonState(bool state)
        {
            deleteBtn.IsEnabled = state;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainWindow.Show();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(rowSelected())
            {
                showDbResult row = (showDbResult)databaseGrid.SelectedItem;

                deleteItemFromDB(row.name);

                game.sendDataToDatabaseView();
            }
        }

        private void showBtn_Click(object sender, RoutedEventArgs e)
        {
            //Kalla på select statement till databas
            game.sendDataToDatabaseView();
        }


        public bool searchfieldEmpty()
        {
            bool res;

            if(searchField.Text.Equals(""))
            {
                res = true;
                searchInfo.Text = "Enter text before searching!";

            } else
            {
                res = false;
            } 

            return res;
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {

            if(!searchfieldEmpty())
            {
                string searchString = searchField.Text;
                game.searchForPlayers(searchString);
            }

        }
    }
}
