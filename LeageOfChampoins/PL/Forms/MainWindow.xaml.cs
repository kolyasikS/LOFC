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

namespace LOFC.PL.Forms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TEST();
        }
        private void TEST()
        {
            /*ClubsWindow clubs = new ClubsWindow();
            clubs.ShowDialog();*/
            /*OwnersWindow owners = new OwnersWindow();
            owners.ShowDialog();*/
            /* CouchesWindow couches = new();
             couches.ShowDialog();*/
            /*  PlayersWindow players = new();
              players.ShowDialog();*/
            Authorization auth = new();
            auth.ShowDialog();
        }
        private void ClubsClick(object sender, RoutedEventArgs e)
        {
            ClubsWindow clubs = new();
            clubs.ShowDialog();
        }

        private void OwnersClick(object sender, RoutedEventArgs e)
        {
            OwnersWindow owners = new();
            owners.ShowDialog();
        }

        private void CouchesClick(object sender, RoutedEventArgs e)
        {
            CouchesWindow couches = new();
            couches.ShowDialog();
        }

        private void LeaguesClick(object sender, RoutedEventArgs e)
        {
            LeaguesWindow leagues = new();
            leagues.ShowDialog();
        }

        private void PlayersClick(object sender, RoutedEventArgs e)
        {
            PlayersWindow players = new();
            players.ShowDialog();
        }
    }
}
