using BLL.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    public class AuthViewModel : ViewModel
    {
        public User User { get; private set; }
        private SecureString _password;
        private string _login;
        public AuthViewModel()
        {
            _login = String.Empty;

            User = new User();
        }
        public SecureString Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }
        public object? GetUser()
        {
            return User.GetUser();
        }
        public void SetUser(dynamic user)
        {
            User.SetUser(user);
        }
    }
}
