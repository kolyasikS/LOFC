using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels.ModalsViewModels
{
    public class PasswordModalViewModel : ViewModel
    {
        private SecureString _securePassword;
        private string _password;
        public PasswordModalViewModel()
        {
            _password = String.Empty;

        }
        public SecureString SecurePassword
        {
            get => _securePassword;
            set => Set(ref _securePassword, value);
        }
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
    }
}
