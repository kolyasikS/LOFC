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
        public LeagueViewModel(Task<IEnumerable<League>> leagues)
        {
            _leagueList = new List<League>(leagues.Result);   
        }
        public IEnumerable<League> LeaguesList
        {
            get => _leagueList;
            set => Set(ref _leagueList, value);
        }
    }
}
