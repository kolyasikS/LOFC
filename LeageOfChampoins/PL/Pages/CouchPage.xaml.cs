using BLL.Services;
using PL.UIProcess;
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
    /// Interaction logic for CouchPage.xaml
    /// </summary>
    public partial class CouchPage : Page
    {
        private CouchService _couchService = new();
        private CouchPageViewModel _couchPageViewModel;

        private List<Label> _labels;
        private List<UIElement> _children;
        private List<UIElement> _mainChildren;
        public CouchPage(int? CouchId)
        {
            InitializeComponent();

            var couch = _couchService.GetCouch(c => c.Id == CouchId).Result.First();
            _couchPageViewModel = new CouchPageViewModel(couch);

            AgeCB.ItemsSource = Enumerable.Range(18, 63).ToList();

            if (CouchId is null)
            {
                _mainChildren = VisibilityPageELement.initMainBoxList("mainBox", mainGrid);
            }
            else
            {
                _labels = VisibilityPageELement.initLabelList("notEditedLabel", mainStacklPanel);
                _children = VisibilityPageELement.initBoxList("EditedBox", mainStacklPanel);
            }

            this.DataContext = _couchPageViewModel;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            updateButton.Visibility = Visibility.Visible;

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

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            updateButton.Visibility = Visibility.Collapsed;
            VisibilityPageELement.SetVisibility(_labels, Visibility.Visible, _children, Visibility.Collapsed);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
