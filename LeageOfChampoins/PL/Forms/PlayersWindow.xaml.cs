using BLL.Entities;
using BLL.Services;
using Newtonsoft.Json;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PlayersWindow.xaml
    /// </summary>
    public partial class PlayersWindow : Window
    {
        private PlayerService _playerService = new();
        private PlayerViewModel _playerViewModel;

        readonly string PATH = $"{Environment.CurrentDirectory}\\JSON data\\Positions.json";

        public PlayersWindow()
        {
            InitializeComponent();

            var players = _playerService.GetPlayers();
            _playerViewModel = new PlayerViewModel(players, FilterOwnerAll, new ClubService().GetClubs().Result.ToList());
            this.DataContext = _playerViewModel;

            InitComboBoxes();
        }
        private void InitComboBoxes()
        {
            if (File.Exists(PATH))
            {
                var positions = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(PATH));
                positionCB.ItemsSource = positions;
            }
        }

        private void ClearFilterButtonClick(object sender, RoutedEventArgs e)
        {
            ClearFilter();
        }
        private void ClearFilter()
        {
            //fromTB.Text = String.Empty;
            //toTB.Text = String.Empty;
            for (int i = 0; i < stackPanelsComboBoxesSP.Children.Count; i++)
            {
                var comboBoxes = (StackPanel)stackPanelsComboBoxesSP.Children[i];
                for (int j = 0; j < comboBoxes.Children.Count; j++)
                    if (comboBoxes.Children[j] is ComboBox)
                    {
                        ((ComboBox)comboBoxes.Children[j]).SelectedIndex = -1;
                    }
            }
            _playerViewModel.SetDefault();

        }
        private void FilterOwnerAll()
        {
            _playerViewModel.FilterPlayerList = _playerViewModel.PlayerList;

            var club = (Club)clubCB.SelectedItem;
            if (club != null)
            {
                _playerViewModel.FilterPlayerList = _playerViewModel.PlayerList.Where(item => item.ClubId == club.Id);
            }
            var position = (string)positionCB.SelectedItem;
            if (position != null)
            {
                _playerViewModel.FilterPlayerList = _playerViewModel.FilterPlayerList.Where(item => item.Position == position);
            }
            var number = (int?)numberCB.SelectedItem;
            if (number != null)
            {
                _playerViewModel.FilterPlayerList = _playerViewModel.FilterPlayerList.Where(item => item.Number == number);
            }

            _playerViewModel.FilterNameTB();
            _playerViewModel.FilterRange();
        }

        private void FilterPlayersCB(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null && ((ComboBox)sender).SelectedIndex != -1)
            {
                //FilterOwnerAll();
            }
        }

        private void FindButtonClick(object sender, RoutedEventArgs e)
        {
            FilterOwnerAll();
        }
    }
}
