using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    public class OwnerViewModel : ViewModel
    {
        private IEnumerable<Owner> _ownerList;
        private IEnumerable<Owner> _filterOwnerList;

        private string _minCapital;
        private string _maxCapital;
        private string _filterName;
        public delegate void filterAll(string value = null);
        private filterAll _filterAllCB;

        public OwnerViewModel(Task<IEnumerable<Owner>> owners, filterAll FilterOwnerAll)
        {
            _ownerList = new List<Owner>(owners.Result);
            
            _filterOwnerList = new List<Owner>(_ownerList);
            _filterAllCB = FilterOwnerAll;
            _filterName = String.Empty;

            _minCapital = "0";
            _maxCapital = _ownerList.OrderByDescending(owner => owner.Capital).Take(1).ToList().First().Capital.ToString();
        }
        public IEnumerable<Owner> OwnerList
        {
            get => _ownerList;
            set
            {
                Set(ref _ownerList, value);
                FilterOwnerList = value;
            }
        }
        public string MinCapital
        {
            get => _minCapital;
            set
            {
                if (double.TryParse(value, out _) || value == string.Empty)
                {
                    Set(ref _minCapital, value);
                    _filterAllCB();
                    FilterRange();
                }
                
            }
        }
        public string MaxCapital
        {
            get => _maxCapital;
            set
            {
                if (double.TryParse(value, out _) || value == string.Empty)
                {
                    Set(ref _maxCapital, value);
                    _filterAllCB();
                }
            }
        }
        public void FilterRange()
        {
           FilterOwnerList = FilterOwnerList.Where(owner => owner.Capital >= Convert.ToDouble(MinCapital) && owner.Capital <= Convert.ToDouble(MaxCapital)).ToList();
        }
        public string FilterName
        {
            get => _filterName;
            set
            {
                Set(ref _filterName, value);
                _filterAllCB(value);
                //FilterNameTB(value);
            }
        }
        public IEnumerable<Owner> FilterOwnerList
        {
            get => _filterOwnerList;
            set => Set(ref _filterOwnerList, value);
        }

        public void FilterNameTB(string? value = null)
        {
            if (value == null)
            {
                value = _filterName;
            }
            FilterOwnerList = FilterOwnerList.Where(item => item.Name.ToLower().StartsWith(value.ToLower()) || value == String.Empty);
        }
    }
}
