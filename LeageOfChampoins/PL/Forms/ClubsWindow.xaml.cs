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

        private void ClearFilter(object sender, RoutedEventArgs e)
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

        }
    }
}
