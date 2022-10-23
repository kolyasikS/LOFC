using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    public class CreatingOwnerViewModel : ViewModel
    {
        //Owner data
        private string _ownerName;
        private string? _surname;
        private string _nation;
        private double _capital;
        private string _logIn;
        private string _password;
        private int _age;
        public CreatingOwnerViewModel()
        {
        }
        public string OwnerName
        {
            get => _ownerName;
            set => Set(ref _ownerName, value);
        }
        public string Surname
        {
            get => _surname ?? string.Empty;
            set => Set(ref _surname, value);
        }
        public string Nation
        {
            get => _nation ?? string.Empty;
            set => Set(ref _nation, value);
        }
        public double Capital
        {
            get => _capital;
            set => Set(ref _capital, value);
        }
        public string LogIn
        {
            get => _logIn;
            set => Set(ref _logIn, value);
        }
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        public int Age
        {
            get => _age;
            set => Set(ref _age, value);
        }
        public List<object> GetOwnerData()
        {
            return new List<object>()
            {
                OwnerName, Surname, Nation, Capital, LogIn, Password, Age
            };
        }
    }
}
