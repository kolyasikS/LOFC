using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    public class CouchViewModel : ViewModel
    {
        private IEnumerable<Couch> _couchList;
        private IEnumerable<Couch> _filterCouchList;
        private IEnumerable<string> _distinctSchemas;

        private string _minSalary;
        private string _maxSalary;
        private string _filterName;
        
        public delegate void filterAll(string value = null);
        private filterAll _filterAllCB;

        private readonly string MAXSALARY;

        public CouchViewModel(Task<IEnumerable<Couch>> couches, filterAll FilterOwnerAll)
        {
            _filterCouchList = _couchList = couches.Result.ToList();
            _filterAllCB = FilterOwnerAll;
            _filterName = String.Empty;

            _minSalary = "0";
            _maxSalary = MAXSALARY = _couchList.OrderByDescending(couch => couch.Salary).Take(1).ToList().First().Salary.ToString();

            _distinctSchemas = new HashSet<string>();
            for (int i = 0; i < _couchList.Count(); i++)
            {
                ((HashSet<string>)_distinctSchemas).Add(_couchList.ToList()[i].MainSchema);
            }

        }
        public IEnumerable<Couch> CouchList
        {
            get => _couchList;
            set
            {
                Set(ref _couchList, value);
                FilterCouchList = value;
            }
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
        public IEnumerable<string> DistinctSchemas
        {
            get => _distinctSchemas;
            set => Set(ref _distinctSchemas, value);
        }
        public IEnumerable<Couch> FilterCouchList
        {
            get => _filterCouchList;
            set => Set(ref _filterCouchList, value);
        }

        public void FilterNameTB(string? value = null)
        {
            if (value == null)
            {
                value = _filterName;
            }
            FilterCouchList = FilterCouchList.Where(item => item.Name.ToLower().StartsWith(value.ToLower()) || value == String.Empty);
        }
        public string MinSalary
        {
            get => _minSalary;
            set
            {
                if (double.TryParse(value, out _) || value == string.Empty)
                {
                    Set(ref _minSalary, value);
                    _filterAllCB();
                    FilterRange();
                }
            }
        }
        public string MaxSalary
        {
            get => _maxSalary;
            set
            {
                if (double.TryParse(value, out _) || value == string.Empty)
                {
                    Set(ref _maxSalary, value);
                    _filterAllCB();
                }
            }
        }
        public void FilterRange()
        {
            FilterCouchList = FilterCouchList.Where(owner => owner.Salary >= Convert.ToDouble(MinSalary)  && owner.Salary <= Convert.ToDouble(MaxSalary)).ToList();
        }
        public void SetDefault()
        {
            FilterCouchList = CouchList;
            MinSalary = "0";
            MaxSalary = MAXSALARY;
        }
    }
}
