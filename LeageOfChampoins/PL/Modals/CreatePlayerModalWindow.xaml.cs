using BLL.Entities;
using Newtonsoft.Json;
using PL.UIProcess;
using PL.ViewModels.ModalsViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace LOFC.PL.Modals
{
    /// <summary>
    /// Interaction logic for CreatePlayerModalWindow.xaml
    /// </summary>
    public partial class CreatePlayerModalWindow : Window
    {
        public Player Player;
        private int _clubId;
        private CreatePlayerModalViewModel _createPlayerModalViewModel = new();
        readonly string PATH = $"{Environment.CurrentDirectory}\\JSON data\\Positions.json";
        public CreatePlayerModalWindow(int ClubId)
        {
            InitializeComponent();

            var labels = VisibilityPageELement.initLabelList("notEditedLabel", mainStacklPanel);
            var children = VisibilityPageELement.initBoxList("EditedBox", mainStacklPanel);
            VisibilityPageELement.SetVisibility(labels, Visibility.Collapsed, children, Visibility.Visible);

            _clubId = ClubId;

            InitComboBoxes();
            this.DataContext = _createPlayerModalViewModel;
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
        private void AddClick(object sender, RoutedEventArgs e)
        {
            if (IsValidData())
            {
                Player = _createPlayerModalViewModel.CreatePlayer(_clubId);
                this.Close();
            }
            else
            {
                MessageBox.Show("Fill all fields!");
            }
        }
        private bool IsValidData()
        {
            PropertyInfo[] properties = typeof(CreatePlayerModalViewModel).GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(_createPlayerModalViewModel);
                try
                {
                    if (value == null)
                    {
                        return false;
                    }
                    if (value?.GetType() == typeof(string) &&
                        (string)value == String.Empty)
                    {
                        return false;
                    }
                    if (value?.GetType() == typeof(int?) &&
                        (int?)value == null)
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
        private void CancelAddClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
