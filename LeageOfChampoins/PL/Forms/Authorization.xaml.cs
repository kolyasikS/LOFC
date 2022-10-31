using BLL.Entities;
using BLL.Services;
using BLL.Users;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private AuthViewModel _authViewModel = new();
        readonly string PATH = $"{Environment.CurrentDirectory}\\JSON data\\SecretaryAccount.json";
        private delegate void SetUser(dynamic user);
        public Authorization()
        {
            InitializeComponent();

            //TEST();
            this.DataContext = _authViewModel;
        }
        public User User
        {
            get => _authViewModel.User;
        }
        public 
        void TEST()
        {

            var password = "LOFCPassword";

            string savedPasswordHash;
            if (!File.Exists(PATH))
            {
                MessageBox.Show("FILE NOT EXISTS");
                return;
            }
            savedPasswordHash = JsonConvert.DeserializeObject<Account>(File.ReadAllText(PATH))?.Password ?? String.Empty;
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    MessageBox.Show("NONE");
                }
            }
            MessageBox.Show(Convert.ToBase64String(hash) + "  --  " + Convert.ToBase64String(hashBytes[16..]));
       
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
        }

        private async void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (_authViewModel.Password != null && _authViewModel.Login != null)
            {
                var pointer = Marshal.SecureStringToGlobalAllocUnicode(_authViewModel.Password);
                var password = Marshal.PtrToStringUni(pointer) ?? String.Empty;


                OwnerService ownerService = new OwnerService();
                AccountService accountService = new AccountService();
                var owners = await ownerService.GetOwners();
                var accounts = await accountService.GetAccounts();
                UserPassword userPassword = new UserPassword(owners, accounts);

                if (userPassword.HasAccount(_authViewModel.Login, password, PATH, _authViewModel.SetUser) != null)
                {
                    this.Close();
                }
                else
                {
                    // such an account does'nt exist
                }
            }
        }
        

        private void AsGuest_Click(object sender, RoutedEventArgs e)
        {
            _authViewModel.SetUser(User.Guest);
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
