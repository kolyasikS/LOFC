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
