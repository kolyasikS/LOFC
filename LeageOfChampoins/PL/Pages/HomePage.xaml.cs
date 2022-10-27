using BLL.Entities;
using BLL.Services;
using PL.ViewModels.PagesViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LOFC.PL.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private HomeViewModel _homeViewModel;
        private LeagueService _leagueService = new();

        private List<Label> _labels;
        private List<UIElement> _children;
        public HomePage(Club club)
        {
            InitializeComponent();

            var leagues = _leagueService.GetLeagues().Result;
            _homeViewModel = new HomeViewModel(club, leagues);
            this.DataContext = _homeViewModel;

            _labels = initLabelList("notEditedLabel");
            _children = initBoxList("EditedBox");
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

        private void EditClick(object sender, RoutedEventArgs e)
        {
            updateButton.Visibility = Visibility.Visible;
            SetVisibility(Visibility.Collapsed, Visibility.Visible);
            
            for (int i = 0; i < posLeagueCB.Items.Count; i++)
            {
                if ((int)posLeagueCB.Items[i] == _homeViewModel.PosInLeague)
                {
                    posLeagueCB.SelectedIndex = i;
                }
                
            }

            for (int i = 0; i < schemaCB.Items.Count; i++)
            {
                var schema = schemaCB.Items[i].ToString()[38..];
                if (schema == _homeViewModel.Schema)
                {
                    schemaCB.SelectedIndex = i;
                }
            }

        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            updateButton.Visibility = Visibility.Collapsed;
            SetVisibility(Visibility.Visible, Visibility.Collapsed);
        }

        private List<Label> initLabelList(string UID)
        {
            List<Label> labelList = new List<Label>();

            for (int i = 0; i < mainStacklPanel.Children.Count; i++)
            {
                if (mainStacklPanel.Children[i] is DockPanel)
                {
                    DockPanel dockPanel = (DockPanel)mainStacklPanel.Children[i];
                    for (int j = 0; j < dockPanel.Children.Count; j++)
                    {
                        if (dockPanel.Children[j] is StackPanel)
                        {
                            StackPanel stackPanel = (StackPanel)dockPanel.Children[j];
                            for (int k = 0; k < stackPanel.Children.Count; k++)
                            {
                                if (stackPanel.Children[k] is Label && stackPanel.Children[k].Uid == UID)
                                {
                                    labelList.Add((Label)stackPanel.Children[k]);
                                }
                            }
                        }
                    }
                }
            }

            return labelList;
        }
        private List<UIElement> initBoxList(string UID)
        {
            List<UIElement> childrenList = new List<UIElement>();

            for (int i = 0; i < mainStacklPanel.Children.Count; i++)
            {
                if (mainStacklPanel.Children[i] is DockPanel)
                {
                    DockPanel dockPanel = (DockPanel)mainStacklPanel.Children[i];
                    for (int j = 0; j < dockPanel.Children.Count; j++)
                    {
                        if (dockPanel.Children[j] is StackPanel)
                        {
                            StackPanel stackPanel = (StackPanel)dockPanel.Children[j];
                            for (int k = 0; k < stackPanel.Children.Count; k++)
                            {
                                if ((stackPanel.Children[k] is Canvas || stackPanel.Children[k] is ComboBox || stackPanel.Children[k] is TextBox) &&
                                     stackPanel.Children[k].Uid == UID)
                                {
                                    childrenList.Add(stackPanel.Children[k]);
                                }
                            }
                        }
                    }
                }
            }

            return childrenList;
        }
        private void SetVisibility(Visibility LabelVis, Visibility ChildVis)
        {
            for (int i = 0; i < _labels.Count; i++)
            {
                _labels[i].Visibility = LabelVis;
            }
            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].Visibility = ChildVis;
            }
        }
    }
}
