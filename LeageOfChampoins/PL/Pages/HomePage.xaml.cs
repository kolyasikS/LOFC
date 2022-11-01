using BLL.Entities;
using BLL.Services;
using PL.UIProcess;
using PL.ViewModels.PagesViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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

namespace LOFC.PL.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private HomePageViewModel _homeViewModel;
        private ClubService _clubService = new();

        private List<Label> _labels;
        private List<UIElement> _children;
        private Club _club;
        public HomePage(Club club)
        {
            InitializeComponent();
            LeagueService _leagueService = new();
            var leagues = _leagueService.GetLeagues().Result;

            _homeViewModel = new HomePageViewModel(club, leagues);
            _club = club;
            
            this.DataContext = _homeViewModel;
            
            _labels = VisibilityPageELement.initLabelList("notEditedLabel", mainStacklPanel);
            _children = VisibilityPageELement.initBoxList("EditedBox", mainStacklPanel);

            
        }
        public async void UpdateCouch(int CouchId)
        {
            _club.CouchId = CouchId;
            await _clubService.UpdateClub(_club);
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

        private async void EditClick(object sender, RoutedEventArgs e)
        {
            SetEditingMode();

            

           //isUpdated = true;
           //this.Close();
        }
        private void SetEditingMode()
        {
            updateButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;
            VisibilityPageELement.SetVisibility(_labels, Visibility.Collapsed, _children, Visibility.Visible);

            for (int i = 0; i < posLeagueCB.Items.Count; i++)
            {
                if ((int)posLeagueCB.Items[i] == _homeViewModel.PosInLeague)
                {
                    posLeagueCB.SelectedIndex = i;
                }

            }

            for (int i = 0; i < schemaCB.Items.Count; i++)
            {
                var schema = schemaCB.Items[i]?.ToString()?[38..];
                if (schema == _homeViewModel.Schema)
                {
                    schemaCB.SelectedIndex = i;
                }
            }
        }
        private async void UpdateClick(object sender, RoutedEventArgs e)
        {
            _homeViewModel.UpdateClub(_club);
            await _clubService.UpdateClub(_club);

            updateButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Visible;
            VisibilityPageELement.SetVisibility(_labels, Visibility.Visible, _children, Visibility.Collapsed);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            _homeViewModel.SetClubData(_club);
            updateButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Visible;
            VisibilityPageELement.SetVisibility(_labels, Visibility.Visible, _children, Visibility.Collapsed);
        }
    }
}
