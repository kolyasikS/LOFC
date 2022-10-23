using BLL.Abstractions.Interfaces;
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
    /// Interaction logic for EditClub.xaml
    /// </summary>
    public partial class EditClub : Window
    {
        public bool isUpdated = false;
        private IClubService _clubService;
        private ILeagueService _leagueService = new LeagueService();
        private Club _club;
        private CreatingClubViewModel _editClubViewModel;
        private ClubViewModel _clubViewModel;
        public EditClub(ClubViewModel clubViewModel, IClubService clubService, Club club)
        {
            InitializeComponent();

            _clubViewModel = clubViewModel;
            _editClubViewModel = new CreatingClubViewModel(_clubViewModel.ClubList, _leagueService.GetLeagues(), club);

            _clubService = clubService;
            _club = club;


            this.DataContext = _editClubViewModel;



            //_editClubViewModel.League = club.League;
            //_editClubViewModel.Schema = club.Schema;
            //_editClubViewModel.PosInLeague = club.posInLeague;
            for (int i = 0; i < schemaCB.Items.Count; i++)
            {
                if ((string)((ComboBoxItem)schemaCB.Items[i]).Content == club.Schema)
                {
                    schemaCB.SelectedIndex = i;
                    break;
                }
            }
            //leagueCB.SelectedIndex = club.LeagueId;
            //posLeagueCB.SelectedIndex = posLeagueCB.Items.IndexOf(club.posInLeague);
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            var clubData = _editClubViewModel.GetClubData();

            _club.Name = (string)clubData[1];
            _club.posInLeague = (int)clubData[2];
            _club.DateFoundation = DateOnly.FromDateTime((DateTime)clubData[3]);
            _club.PosUEFARatingClubs = (int)clubData[4];
            _club.Cost = (double)clubData[5];
            _club.AmountTrophies = (int)clubData[6];
            _club.Schema = (string)clubData[7];
            _club.LeagueId = new List<League>(_leagueService.GetLeague(item => item.Name == (string)clubData[8]).Result)[0].Id;
            _club.Country = new List<League>(_leagueService.GetLeague(item => item.Name == (string)clubData[8]).Result)[0].Country;

            _clubService.UpdateClub(_club);

            isUpdated = true;
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LeagueSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((League)((ComboBox)sender).SelectedItem != null)
            {
                var league = (League)((ComboBox)sender).SelectedItem;
                posLeagueCB.ItemsSource = Enumerable.Range(1, league.AmountClubs).ToList();
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var dateTime = ((DatePicker)sender).SelectedDate;
            dateLabel.Content = dateTime?.ToString("d", CultureInfo.GetCultureInfo("de-De"));
        }
    }
}
