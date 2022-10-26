using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    public class PlayerViewModel : ViewModel
    {
        private IEnumerable<Player> _playerList;
        private IEnumerable<Player> _filterPlayerList;

        private IEnumerable<Club> _distinctClubs;
        private IEnumerable<int> _distinctNumbers;

        private string _minSalary;
        private string _maxSalary;
        private string _filterName;
        private string _filterSurName;

        public delegate void filterAll();
        private filterAll _filterAllCB;

        private readonly string MAXSALARY;

        public PlayerViewModel(Task<IEnumerable<Player>> players, filterAll FilterOwnerAll, List<Club> clubs)
        {
            _filterPlayerList = _playerList = players.Result.ToList();
            _filterAllCB = FilterOwnerAll;
            _filterName = String.Empty;
            _filterSurName = String.Empty;

            _minSalary = "0";
            _maxSalary = MAXSALARY = _playerList.OrderByDescending(player => player.Salary).First().Salary.ToString();

            _distinctClubs = new HashSet<Club>();
            for (int i = 0; i < _playerList.Count(); i++)
            {
                ((HashSet<Club>)_distinctClubs).Add(clubs.Find(c => c.Id == _playerList.ToList()[i].ClubId) ?? new Club());
            }

            _distinctNumbers = new HashSet<int>();
            for (int i = 0; i < _playerList.Count(); i++)
            {
                ((HashSet<int>)_distinctNumbers).Add(_playerList.ToList()[i].Number);
            }
        }
        public IEnumerable<Player> PlayerList
        {
            get => _playerList;
            set
            {
                Set(ref _playerList, value);
                FilterPlayerList = value;
            }
        }
        public string FilterName
        {
            get => _filterName;
            set
            {
                Set(ref _filterName, value);
                _filterAllCB();
                //FilterNameTB(value);
            }
        }
        public string FilterSurName
        {
            get => _filterSurName;
            set
            {
                Set(ref _filterSurName, value);
                _filterAllCB();
                //FilterNameTB(value);
            }
        }
        public IEnumerable<Player> FilterPlayerList
        {
            get => _filterPlayerList;
            set => Set(ref _filterPlayerList, value);
        }
        public IEnumerable<Club> DistinctClubs
        {
            get => _distinctClubs;
            set => Set(ref _distinctClubs, value);
        }
        public IEnumerable<int> DistinctNumbers
        {
            get => _distinctNumbers;
            set => Set(ref _distinctNumbers, value);
        }
        public void FilterNameTB()
        {
            FilterPlayerList = FilterPlayerList
                .Where(item => item.Name.ToLower().StartsWith(_filterName.ToLower()) || _filterName == String.Empty);

            FilterPlayerList = FilterPlayerList
                .Where(item => item.Surname.ToLower().StartsWith(_filterSurName.ToLower()) || _filterSurName == String.Empty);
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
                    FilterRange();
                }
            }
        }
        public void FilterRange()
        {
            FilterPlayerList = FilterPlayerList.Where(owner => owner.Salary >= Convert.ToDouble(MinSalary) && owner.Salary <= Convert.ToDouble(MaxSalary)).ToList();
        }
        public void SetDefault()
        {
            FilterPlayerList = PlayerList;
            MinSalary = "0";
            MaxSalary = MAXSALARY;

            FilterSurName = String.Empty;
            FilterName = String.Empty;
        }
    }
}
