using BLL.Entities;
using BLL.Services;
using LOFC.PL.Modals;
using Newtonsoft.Json;
using PL.UIProcess;
using PL.ViewModels.PagesViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LOFC.PL.Pages
{
    /// <summary>
    /// Interaction logic for PlayersPage.xaml
    /// </summary>
    public partial class PlayersPage : Page
    {
        private PlayerService _playerService = new();
        private PlayerPageViewModel _playerPageViewModel;

        private int _clubId;

        private List<Label> _labels;
        private List<UIElement> _children;
        private List<UIElement> _mainChildren;

        readonly string PATH = $"{Environment.CurrentDirectory}\\JSON data\\Positions.json";

        public PlayersPage(List<Player>? players, int ClubId)
        {
            InitializeComponent();
            _playerPageViewModel = new PlayerPageViewModel(players);

            _labels = VisibilityPageELement.initLabelList("notEditedLabel", mainStacklPanel);
            _children = VisibilityPageELement.initBoxList("EditedBox", mainStacklPanel);
            _mainChildren = VisibilityPageELement.initMainBoxList("mainBox", mainGrid);

            _clubId = ClubId;
            if (players.Count == 0)
            {
                VisibilityPageELement.SetVisibility(havingNotCouch, Visibility.Visible);
                VisibilityPageELement.SetVisibility(_mainChildren, Visibility.Collapsed);
            }

            this.DataContext = _playerPageViewModel;

            InitComboBoxes();
        }
        
        private void InitComboBoxes()
        {
            if (File.Exists(PATH))
            {
                var positions = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(PATH));
                positionCB.ItemsSource = positions;
            }

            AgeCB.ItemsSource = Enumerable.Range(18, 63).ToList();

        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            SetEditingMode();
        }
        private void SetEditingMode()
        {
            updateButton.Visibility = Visibility.Visible;

            VisibilityPageELement.SetVisibility(_labels, Visibility.Collapsed, _children, Visibility.Visible);

            for (int i = 0; i < AgeCB.Items.Count; i++)
            {
                if ((int)AgeCB.Items[i] == _playerPageViewModel.Age)
                {
                    AgeCB.SelectedIndex = i;
                }

            }
            for (int i = 0; i < positionCB.Items.Count; i++)
            {
                var position = positionCB.Items[i]?.ToString();
                if (position == _playerPageViewModel.Position)
                {
                    positionCB.SelectedIndex = i;
                }
            }
        }
        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            updateButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            VisibilityPageELement.SetVisibility(_labels, Visibility.Visible, _children, Visibility.Collapsed);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            _playerPageViewModel.SetPlayer();
            updateButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            VisibilityPageELement.SetVisibility(_labels, Visibility.Visible, _children, Visibility.Collapsed);
        }

        private async void AppointClick(object sender, RoutedEventArgs e)
        {
           await CreatePlayer();
        }
        private async Task CreatePlayer()
        {
            CreatePlayerModalWindow createPlayerModal = new(_clubId);
            createPlayerModal.ShowDialog();

            if (createPlayerModal.Player != null)
            {
                await _playerService.CreatePlayer(createPlayerModal.Player);
                _playerPageViewModel.AddPlayer(createPlayerModal.Player);

                VisibilityPageELement.SetVisibility(havingNotCouch, Visibility.Collapsed);
                VisibilityPageELement.SetVisibility(_mainChildren, Visibility.Visible);
            }
            else
            {

            }
        }
        private async void SignClick(object sender, RoutedEventArgs e)
        {
            await CreatePlayer();
        }

        /* ------------------------------------------------------- */
    }
}
