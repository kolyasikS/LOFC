using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels.PagesViewModels
{
    public class OwnerPageViewModel : ViewModel
    {
        //Owner data
        private string _ownerName;
        private string? _surname;
        private string _nation;
        private double _capital;
        private int _age;
        public OwnerPageViewModel(Owner owner)
        {
            SetOwner(owner);
        }
        public void SetOwner(Owner owner)
        {
            OwnerName = owner.Name;
            Surname = owner.Surname ?? String.Empty;
            Capital = owner.Capital;
            Age = owner.Age;
            Nation = owner.Nation;
        }
        public void UpdateOwner(Owner owner)
        {
            owner.Name = OwnerName;
            owner.Surname = Surname;
            owner.Capital = Capital;
            owner.Age = Age;
            owner.Nation = Nation;
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
        public int Age
        {
            get => _age;
            set => Set(ref _age, value);
        }
    }

}
