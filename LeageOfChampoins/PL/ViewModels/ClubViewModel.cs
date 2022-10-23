using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    public class ClubViewModel : ViewModel
    {
        private IEnumerable<Club> _clubList;
        private IEnumerable<Club> _filterClubList;
        private IEnumerable<string> _distinctSchemas;
        private string _filterName;
        private filterAll _filterAllCB;
        public delegate void filterAll();
        public ClubViewModel(Task<IEnumerable<Club>> clubs, filterAll FilterClubAll)
        {
            _clubList = new List<Club>(clubs.Result);
            _filterClubList = new List<Club>(_clubList);
            _distinctSchemas = new HashSet<string>();
            _filterName = String.Empty;
            _filterAllCB = FilterClubAll;

            for (int i = 0; i < _clubList.Count(); i++)
            {
                ((HashSet<string>)_distinctSchemas).Add(((List<Club>)_clubList)[i].Schema);
            }
        }
        public IEnumerable<Club> ClubList
        {
            get => _clubList;
            set
            {
                Set(ref _clubList, value);
                FilterClubList =  value;
            }
        }
        public IEnumerable<Club> FilterClubList
        {
            get => _filterClubList;
            set => Set(ref _filterClubList, value);
        }
        public IEnumerable<string> DistinctSchemas
        {
            get => _distinctSchemas;
            set => Set(ref _distinctSchemas, value);
        }

        public string FilterName
        {
            get => _filterName;
            set
            {
                Set(ref _filterName, value);
                //FilterClubList = _clubList.Where(item => item.Name.ToLower().IndexOf(value.ToLower(), 0) != -1 || value == String.Empty);
                _filterAllCB();
                FilterNameTB(value);
            }
        }
        public void FilterNameTB(string? value = null)
        {
            if (value == null)
            {
                value = _filterName;
            }
            FilterClubList = FilterClubList.Where(item => item.Name.ToLower().StartsWith(value.ToLower()) || value == String.Empty);
        }
    }
}
