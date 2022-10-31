using BLL.Entities;
using BLL.Services;
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

        private List<Label> _labels;
        private List<UIElement> _children;
        private List<UIElement> _mainChildren;

        readonly string PATH = $"{Environment.CurrentDirectory}\\JSON data\\Positions.json";

        public PlayersPage(List<Player> players)
        {
            InitializeComponent();
            _playerPageViewModel = new PlayerPageViewModel(players);

            if (players is null)
            {
                _mainChildren = VisibilityPageELement.initMainBoxList("mainBox", mainGrid);
                VisibilityPageELement.SetVisibility(havingNotCouch, Visibility.Visible);
                VisibilityPageELement.SetVisibility(_mainChildren, Visibility.Collapsed);
            }
            else
            {
                _labels = VisibilityPageELement.initLabelList("notEditedLabel", mainStacklPanel);
                _children = VisibilityPageELement.initBoxList("EditedBox", mainStacklPanel);
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
            VisibilityPageELement.SetVisibility(_labels, Visibility.Visible, _children, Visibility.Collapsed);
        }

    }
}
