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
    /// Interaction logic for CouchPage.xaml
    /// </summary>
    public partial class CouchPage : Page
    {
        private CouchService _couchService = new();
        private Couch? _couch;
        private CouchPageViewModel _couchPageViewModel;

        private List<Label> _labels;
        private List<UIElement> _children;
        private List<UIElement> _mainChildren;
        private Action<int> _UpdateCouch; 
        public CouchPage(int? CouchId, Action<int> UpdateCouch)
        {
            InitializeComponent();

            _couch = CouchId != null ? _couchService.GetCouch(c => c.Id == CouchId).Result.First() : null;
            _couchPageViewModel = new CouchPageViewModel(_couch);
            _UpdateCouch = UpdateCouch;

            AgeCB.ItemsSource = Enumerable.Range(18, 63).ToList();

            _mainChildren = VisibilityPageELement.initMainBoxList("mainBox", mainGrid);
            _labels = VisibilityPageELement.initLabelList("notEditedLabel", mainStacklPanel);
            _children = VisibilityPageELement.initBoxList("EditedBox", mainStacklPanel);

            if (CouchId == null)
            {
                havingNotCouch.Visibility = Visibility.Visible;
                VisibilityPageELement.SetVisibility(_mainChildren, Visibility.Collapsed);
            }

            this.DataContext = _couchPageViewModel;
        }
        private void EditClick(object sender, RoutedEventArgs e)
        {
            SetEditingMode();
        }
        private void SetEditingMode()
        {
            updateButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Visible;

            VisibilityPageELement.SetVisibility(_labels, Visibility.Collapsed, _children, Visibility.Visible);

            for (int i = 0; i < AgeCB.Items.Count; i++)
            {
                if ((int)AgeCB.Items[i] == _couchPageViewModel.Age)
                {
                    AgeCB.SelectedIndex = i;
                }

            }
            for (int i = 0; i < schemaCB.Items.Count; i++)
            {
                var schema = schemaCB.Items[i]?.ToString()?[38..];
                if (schema == _couchPageViewModel.Schema)
                {
                    schemaCB.SelectedIndex = i;
                }
            }
        }
        private async void UpdateClick(object sender, RoutedEventArgs e)
        {
            _couchPageViewModel.UpdateCouch(_couch);
            await _couchService.UpdateCouch(_couch);
            
            cancelButton.Visibility = Visibility.Collapsed;
            updateButton.Visibility = Visibility.Collapsed;
            VisibilityPageELement.SetVisibility(_labels, Visibility.Visible, _children, Visibility.Collapsed);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            _couchPageViewModel.SetCouch(_couch);
#pragma warning restore CS8604 // Possible null reference argument.
            updateButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;
            VisibilityPageELement.SetVisibility(_labels, Visibility.Visible, _children, Visibility.Collapsed);
        }
        private bool IsValidData()
        {
            PropertyInfo[] properties = typeof(CouchPageViewModel).GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(_couchPageViewModel);
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
                    if (value?.GetType() == typeof(DateTime?) &&
                        (DateTime?)value == null)
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
        private async void AddClick(object sender, RoutedEventArgs e)
        {
            if (IsValidData())
            {
                _couch = _couchPageViewModel.CreateCouch();
                await _couchService.CreateCouch(_couch);
                _couchPageViewModel.SetCouch(_couch);

                _UpdateCouch(_couch.Id);

                couchCard.Margin = new Thickness(5, 5, 5, 5);
                AddDP.Visibility = Visibility.Collapsed;
                EditDP.Visibility = Visibility.Visible;

                VisibilityPageELement.SetVisibility(_labels, Visibility.Visible, _mainChildren, Visibility.Visible);
                VisibilityPageELement.SetVisibility(_children, Visibility.Collapsed);
            }
            else
            {
                MessageBox.Show("Fill all fields!");
            }
        }

        private void AppointClick(object sender, RoutedEventArgs e)
        {
            couchCard.Visibility = Visibility.Visible;
            couchCard.Margin = new Thickness(5, 20, 5, 5);
            AddDP.Visibility = Visibility.Visible;

            havingNotCouch.Visibility = Visibility.Collapsed;
            VisibilityPageELement.SetVisibility(_labels, Visibility.Collapsed, _children, Visibility.Visible);
        }
        private void CancelAddClick(object sender, RoutedEventArgs e)
        {
            havingNotCouch.Visibility = Visibility.Visible;
            VisibilityPageELement.SetVisibility(_children, Visibility.Collapsed);

            _couchPageViewModel.SetDefault();

            couchCard.Visibility = Visibility.Collapsed;
            AddDP.Visibility = Visibility.Collapsed;
        }
    }
}
