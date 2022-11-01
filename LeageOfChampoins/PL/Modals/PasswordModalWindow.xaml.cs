using BLL.Entities;
using BLL.Users;
using PL.ViewModels.ModalsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for PasswordModalWindow.xaml
    /// </summary>
    public partial class PasswordModalWindow : Window
    {
        public string newPassword;
        private Account _account;
        private PasswordModalViewModel _passwordModalViewModel;

        public PasswordModalWindow(Account account)
        {
            InitializeComponent();

            _passwordModalViewModel = new PasswordModalViewModel();
            newPassword = String.Empty;

            _account = account;

            this.DataContext = _passwordModalViewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {


            if (UserPassword.IsDataCoincide(_passwordModalViewModel.Password, _account.Password))
            {
                newPassword = UserPassword.ToHashedPassword(_passwordModalViewModel.SecurePassword);
                this.Close();
            }
            else
            {
                MessageBox.Show("Passwords don't match.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
