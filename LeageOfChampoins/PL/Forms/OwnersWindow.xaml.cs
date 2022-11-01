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
    /// Interaction logic for OwnersWindow.xaml
    /// </summary>
    public partial class OwnersWindow : Window
    {
        private readonly OwnerService _ownerService = new OwnerService();
        private readonly OwnerViewModel _ownerViewModel;
        public OwnersWindow()
        {
            InitializeComponent();

            var owners = _ownerService.GetOwners();
            _ownerViewModel = new OwnerViewModel(owners, FilterOwnerAll);
            this.DataContext = _ownerViewModel;
        }

        private void ClearFilterButtonClick(object sender, RoutedEventArgs e)
        {
            _ownerViewModel.FilterOwnerList = _ownerViewModel.OwnerList;
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
            _ownerViewModel.FilterOwnerList = _ownerViewModel.OwnerList;
            var owner = (Owner)countryCB.SelectedItem;
            if (owner != null)
            {
                _ownerViewModel.FilterOwnerList = _ownerViewModel.OwnerList.Where(item => item.Nation == owner.Nation);
            }
            _ownerViewModel.FilterNameTB(value);
            _ownerViewModel.FilterRange();
        }
    }
}
