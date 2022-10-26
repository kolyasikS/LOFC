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
        public object? GetUser()
        {
            return _authViewModel.GetUser();
        }
        void TEST()
        {

            var password = "LOFCPassword";
            /*  
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            string savedPasswordHash = DBContext.GetUser(u => u.UserName == user).Password;*/
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
            /*
            if (File.Exists(PATH))
            {
                var jsonAccount = JsonConvert.SerializeObject(new Account()
                {
                    LogIn = "LOFCSecretary",
                    Password = savedPasswordHash,
                });
                File.WriteAllText(PATH, jsonAccount);
            }*/
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).SecurePassword;
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (_authViewModel.Password != null && _authViewModel.Login != null)
            {
                var pointer = Marshal.SecureStringToGlobalAllocUnicode(_authViewModel.Password);
                var password = Marshal.PtrToStringUni(pointer) ?? String.Empty;

                if (!isLogInFailed(_authViewModel.Login, password, _authViewModel.SetUser).Result)
                {
                    this.Close();
                }
            }
           /* var pointer = Marshal.SecureStringToGlobalAllocUnicode(_authViewModel.Password);
            var password = Marshal.PtrToStringUni(pointer) ?? String.Empty;

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 16));
            Console.WriteLine($"Hashed: {hashed}");*/

        }
        private async Task<bool> isLogInFailed(string login, string password, SetUser? SetUser = null)
        {
            if (!File.Exists(PATH))
            {
                //MessageBox.Show("FILE NOT EXISTS");
                //return true;
                var secretaryAccount = JsonConvert.DeserializeObject<Account>(File.ReadAllText(PATH));
                if (isDataCoincide(login, password, secretaryAccount?.LogIn, secretaryAccount?.Password ?? String.Empty))
                {
                    if (SetUser != null)
                    {
                        SetUser(User.Secretary);
                    }
                    return false;
                }
            }
            OwnerService ownerService = new OwnerService();
            AccountService accountService = new AccountService();
            var owners = await ownerService.GetOwners();//.Result.ToList();
            var accounts = await accountService.GetAccounts();//.Result.ToList();

            var result = owners.Join(accounts, own => own.AccountId, acc => acc.Id, (own, acc) => new { own, acc }).ToList();
            
            for (int i = 0; i < result.Count; i++)
            {
                if (isDataCoincide(login, password, result[i].acc.LogIn, result[i].acc.Password))
                {
                    if (SetUser != null)
                    {
                        SetUser(result[i]);
                    }
                    return false;
                }
            }

            return true;
        }
        private bool isDataCoincide(string newLogin, string newPass, string? savedLogin, string savedPass)
        {

            
            byte[] hashBytes;
            try
            {
                hashBytes = Convert.FromBase64String(savedPass);
            }
            catch (Exception)
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(savedPass);
                savedPass = Convert.ToBase64String(plainTextBytes);
                hashBytes = Convert.FromBase64String(savedPass);
            }

            byte[] salt = new byte[16];

            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(newPass, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            if (newLogin != savedLogin)
            {
                return false;
            }
            return true;
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
