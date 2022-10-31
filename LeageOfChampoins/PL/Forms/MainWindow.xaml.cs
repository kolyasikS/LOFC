using BLL.Entities;
using BLL.Services;
using BLL.Users;
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
        private Club? _club;
        private User _user;
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
            ClubService clubService = new();
            var club = clubService.GetClub(c => c.Id == 24).Result.First();
            OwnerService ownerService = new();
            var owner = ownerService.GetOwner(o => o.Id == 22).Result.First();
            ClubManagement clubManagement = new ClubManagement(club, owner);
            clubManagement.ShowDialog();
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

        private void ManageClub(object sender, RoutedEventArgs e)
        {
            ClubManagement clubManagement = new ClubManagement(_club, _user.GetOwnerUser());
            clubManagement.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           /* Authorization authorization = new Authorization();
            authorization.ShowDialog();
            try
            {
                var user = authorization.User;
                if (user.GetOwnerUser() != null)
                {
                    _user.SetUser(user.GetOwnerUser());

                    _club = SetClub(_user.GetOwnerUser());
                }
                else
                {
                    _user.SetUser(user.GetUser() ?? String.Empty);
                    _club = null;
                }

            }
            catch (Exception ex)
            {
                _club = null;
            }*/
        }
        private Club SetClub(Owner owner)
        {
            ClubService clubService = new();
            var club = clubService.GetClub(c => c.Id == owner.ClubId).Result.First();
            return club;
        }
    }
}
