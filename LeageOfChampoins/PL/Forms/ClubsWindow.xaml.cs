using BLL.Entities;
using BLL.Services;
using DAL.DataBase;
using DAL.Services;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Clubs.xaml
    /// </summary>
    public partial class ClubsWindow : Window
    {
        //private readonly ClubService _clubsService = new ClubService();
        private readonly ClubService _clubService = new ClubService();
        private readonly OwnerService _ownerService = new OwnerService();
        private readonly ClubViewModel _clubViewModel;
        public ClubsWindow()
        {
            InitializeComponent();

            var clubs = _clubService.GetClubs();
            _clubViewModel = new ClubViewModel(clubs, FilterClubAll);
            this.DataContext = _clubViewModel;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ClearFilterButtonClick(object sender, RoutedEventArgs e)
        {
            ClearFilter();
        }
        private void ClearFilter()
        {
            _clubViewModel.FilterClubList = _clubViewModel.ClubList;
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
                FilterClubAll();
            }

        }
        private void FilterClubAll()
        {
            _clubViewModel.FilterClubList = _clubViewModel.ClubList;
            var club = (Club)countryCB.SelectedItem;
            if (club != null)
            {
                _clubViewModel.FilterClubList = _clubViewModel.ClubList.Where(item => item.Country == club.Country);
            }
            var schema = (string)schemaCB.SelectedItem;
            if (schema != null)
            {
                _clubViewModel.FilterClubList = _clubViewModel.FilterClubList.Where(item => item.Schema == schema);
            }
            _clubViewModel.FilterNameTB();
        }

        private void CreateClub(object sender, RoutedEventArgs e)
        {
            CreatingClub creatingClub = new CreatingClub(_clubViewModel, _clubService);
            creatingClub.ShowDialog();
            if (creatingClub.isCreated)
            {
                _clubViewModel.ClubList = new List<Club>(_clubService.GetClubs().Result);
                FilterClubAll();
            }
        }

        private void GetTopByCapitalOfOwner(object sender, RoutedEventArgs e)
        {
            ClearFilter();
            var topOwners = new List<Owner>(_ownerService.GetOwners(query => (IOrderedQueryable<Owner>)query.OrderBy(owner => owner.Capital).Take(10)).Result);
            _clubViewModel.FilterClubList = new List<Club>(_clubViewModel.ClubList.Where(_club => topOwners.Exists(owner => owner.ClubId == _club.Id)));
        }

        private async void DeleteClub(object sender, RoutedEventArgs e)
        {
            var club = ClubsTable.SelectedItem;
            await _clubService.DeleteClub((Club)club);
            _clubViewModel.ClubList = new List<Club>(_clubService.GetClubs().Result);
        }

        private void EditClub(object sender, RoutedEventArgs e)
        {
            var club = ClubsTable.SelectedItem;
            EditClub edit = new EditClub(_clubViewModel, _clubService, (Club)club);
            edit.ShowDialog();
            if (edit.isUpdated)
            {
                _clubViewModel.ClubList = new List<Club>(_clubService.GetClubs().Result);
                FilterClubAll();
            }
        }
    }
}
