using BLL.Entities;
using BLL.Services;
using PL.ViewModels;
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

namespace LOFC.PL.Forms
{
    /// <summary>
    /// Interaction logic for CouchesWindow.xaml
    /// </summary>
    public partial class CouchesWindow : Window
    {
        private CouchService _couchService = new();
        private CouchViewModel _couchViewModel;
        public CouchesWindow()
        {
            InitializeComponent();

            var couches = _couchService.GetCouches();
            _couchViewModel = new CouchViewModel(couches, FilterOwnerAll);
            this.DataContext = _couchViewModel;
        }

        private void ClearFilterButtonClick(object sender, RoutedEventArgs e)
        {
            ClearFilter();
        }
        private void ClearFilter()
        {
                //fromTB.Text = String.Empty;
                //toTB.Text = String.Empty;
                _couchViewModel.SetDefault();
                for (int i = 0; i < stackPanelsComboBoxesSP.Children.Count; i++)
                {
                    var comboBoxes = (StackPanel)stackPanelsComboBoxesSP.Children[i];
                    for (int j = 0; j < comboBoxes.Children.Count; j++)
                        if (comboBoxes.Children[j] is ComboBox)
                        {
                            ((ComboBox)comboBoxes.Children[j]).SelectedIndex = -1;
                        }
                }

        }
        private void FilterClubsCB(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null)
            {
                FilterOwnerAll();
            }
        }
        private void FilterOwnerAll(string value = null)
        {
            _couchViewModel.FilterCouchList = _couchViewModel.CouchList;
            var couchSchema = (string)schemaCB.SelectedItem;
            if (couchSchema != null)
            {
                _couchViewModel.FilterCouchList = _couchViewModel.CouchList.Where(item => item.MainSchema == couchSchema);
            }
            _couchViewModel.FilterNameTB(value);
            _couchViewModel.FilterRange();
        }

        private void GetTopCouches(object sender, RoutedEventArgs e)
        {
            ClearFilter();

            var clubService = new ClubService();
            var playerService = new PlayerService();

            var players = playerService.GetPlayers().Result.ToList();
            var clubs = clubService.GetClubs().Result.ToList();
            var topAssistClubs = clubs.OrderByDescending(club => players.Where(p => club.Id == p.ClubId).Aggregate(0, (acc, p) => acc + p.Assists)).Take(5).ToList();
            var topCouches = topAssistClubs.Select(club => _couchViewModel.CouchList.ToList().Find(couch => couch.Id == club.CouchId));
            _couchViewModel.FilterCouchList = (IEnumerable<Couch>)topCouches;
        }
    }
}
