using BLL.Entities;
using BLL.Services;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace LOFC.PL.Forms
{
    /// <summary>
    /// Interaction logic for CreatingClub.xaml
    /// </summary>
    public partial class CreatingClub : Window
    {
        private readonly ClubViewModel _clubViewModel;
        private readonly CreatingClubViewModel _creatingClubViewModel;
        private readonly LeagueService _leagueService = new LeagueService();
        private readonly OwnerService _ownerService = new OwnerService();
        private readonly ClubService _clubService;

        public bool isCreated = false;
        public CreatingClub(ClubViewModel clubViewModel, ClubService clubService)
        {
            InitializeComponent();

            AgeCB.ItemsSource = Enumerable.Range(18, 63).ToList();
            _clubViewModel = clubViewModel;
            _clubService = clubService;
            _creatingClubViewModel = new CreatingClubViewModel(_clubViewModel.ClubList, _leagueService.GetLeagues());

            this.DataContext = _creatingClubViewModel;
            
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Create(object sender, RoutedEventArgs e)
        {
            AccountService accountService = new AccountService();
            var ownerData = _creatingClubViewModel.GetOwnerData();
            var clubData = _creatingClubViewModel.GetClubData();
            if ((bool)clubData[0])
            {
                return; // warning - null
            }
            int clubId = await _clubService.CreateClub(new Club
            {
                Name = (string)clubData[1],
                posInLeague = (int)clubData[2],
                DateFoundation = DateOnly.FromDateTime((DateTime)clubData[3]),
                PosUEFARatingClubs = (int)clubData[4],
                Cost = (double)clubData[5],
                AmountTrophies = (int)clubData[6],
                Schema = (string)clubData[7],
                LeagueId = new List<League>(_leagueService.GetLeague(item => item.Name == (string)clubData[8]).Result)[0].Id,
                Country = new List<League>(_leagueService.GetLeague(item => item.Name == (string)clubData[8]).Result)[0].Country,
            });
            int accountId = await accountService.CreateAccount(new Account
            {
                LogIn = (string)ownerData[4],
                Password = (string)ownerData[5]
            });
            await _ownerService.CreateOwner(new Owner
            {
                Name = (string)ownerData[0],
                Surname = (string)ownerData[1],
                Nation = (string)ownerData[2],
                Capital = (double)ownerData[3],
                Age = (int)ownerData[6],
                AccountId = accountId,
                ClubId = clubId,
            });

            isCreated = true;
            this.Close();
        }

        private void LeagueSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var league = (League)((ComboBox)sender).SelectedItem;
            posLeagueCB.ItemsSource = Enumerable.Range(1, league.AmountClubs).ToList();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var dateTime = ((DatePicker)sender).SelectedDate;
            dateLabel.Content = dateTime?.ToString("d", CultureInfo.GetCultureInfo("de-De"));
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _creatingClubViewModel.OwnerViewModel.Password = ((PasswordBox)sender).Password;
        }
    }
}
