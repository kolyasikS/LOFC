using BLL.Entities;
using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModels
{
    public class LeagueViewModel : ViewModel
    {
        private IEnumerable<League> _leagueList;
        private IEnumerable<League> _filterLeagueList;

        private string _filterName;

        public LeagueViewModel(Task<IEnumerable<League>> leagues)
        {
           _filterLeagueList = _leagueList = new List<League>(leagues.Result);   
        }
        public IEnumerable<League> LeaguesList
        {
            get => _leagueList;
            set 
            {
                Set(ref _leagueList, value);
                FilterLeagueList = value;
            }
        }
        public IEnumerable<League> FilterLeagueList
        {
            get => _filterLeagueList;
            set => Set(ref _filterLeagueList, value);
        }
        public string FilterName
        {
            get => _filterName;
            set
            {
                Set(ref _filterName, value);
                FilterLeagueList = LeaguesList.Where(item => item.Name.ToLower().StartsWith(value.ToLower()) || value == String.Empty);

            }
        }
    }
}
