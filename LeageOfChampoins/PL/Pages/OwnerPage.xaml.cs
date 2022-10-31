using BLL.Entities;
using BLL.Services;
using LOFC.PL.Modals;
using PL.UIProcess;
using PL.ViewModels.PagesViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LOFC.PL.Pages
{
    /// <summary>
    /// Interaction logic for OwnerPage.xaml
    /// </summary>
    public partial class OwnerPage : Page
    {
        private AccountService _accountService = new();
        private Account _account;

        private OwnerPageViewModel _ownerPageViewModel;
        private OwnerService _ownerService = new();
        private Owner _owner;
      
        private List<Label> _labels;
        private List<UIElement> _children;
        public OwnerPage(Owner owner)
        {
            InitializeComponent();

            AgeCB.ItemsSource = Enumerable.Range(18, 63).ToList();
            _ownerPageViewModel = new OwnerPageViewModel(owner);

            _owner = owner;

            _labels = VisibilityPageELement.initLabelList("notEditedLabel", mainStacklPanel);
            _children = VisibilityPageELement.initBoxList("EditedBox", mainStacklPanel);

            _account = _accountService.GetAccount(ac => ac.Id == _owner.AccountId).Result.First();

            this.DataContext = _ownerPageViewModel;
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
                if ((int)AgeCB.Items[i] == _ownerPageViewModel.Age)
                {
                    AgeCB.SelectedIndex = i;
                }

            }
        }
        private void SetViewingMode()
        {
            _ownerPageViewModel.SetOwner(_owner);
            updateButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Visible;
            VisibilityPageELement.SetVisibility(_labels, Visibility.Visible, _children, Visibility.Collapsed);
        }
        private async void UpdateClick(object sender, RoutedEventArgs e)
        {
            _ownerPageViewModel.UpdateOwner(_owner);
            await _ownerService.UpdateOwner(_owner);

            updateButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Visible;
            VisibilityPageELement.SetVisibility(_labels, Visibility.Visible, _children, Visibility.Collapsed);
        }

        private async void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            PasswordModalWindow passwordModal = new PasswordModalWindow(_account);
            passwordModal.ShowDialog();

            if (passwordModal.newPassword != String.Empty)
            {
                _account.Password = passwordModal.newPassword;
                await _accountService.UpdateAccount(_account);
            }
            SetViewingMode();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            SetViewingMode();
        }
    }
}
