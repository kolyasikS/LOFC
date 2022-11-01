using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels
{
    public class CreatingClubViewModel :ViewModel
    {
        private IEnumerable<Club> _clubList;
        private IEnumerable<League> _leagueList;
        private CreatingOwnerViewModel _ownerViewModel;
       

        //Club Data
        private string _clubName;
        private int _posInUEFA;
        private int _posInLeague;
        private double _cost;
        private int _amountTrophies;
        
        private DateTime? _dateFoundation;

        private string _schema;
        private League? _league;
        public CreatingClubViewModel(IEnumerable<Club> clubs, Task<IEnumerable<League>> leagues)
        {
            InitLists(clubs, leagues);
            _ownerViewModel = new CreatingOwnerViewModel();
        }
        public CreatingClubViewModel(IEnumerable<Club> clubs, Task<IEnumerable<League>> leagues, Club club)
        {
            InitLists(clubs, leagues);
            ClubName = club.Name;
            PosInUEFA = club.PosUEFARatingClubs;
            Cost = club.Cost;
            AmountTrophies = club.AmountTrophies;
            DateFoundation = club.DateFoundation.ToDateTime(new TimeOnly(0));
            PosInLeague = club.posInLeague;
            Schema = club.Schema;
            League = new List<League>(LeagueList).Find(item => item.Id == club.LeagueId);
        }
        private void InitLists(IEnumerable<Club> clubs, Task<IEnumerable<League>> leagues)
        {
            _clubList = clubs;
            _leagueList = leagues.Result;

            _dateFoundation = DateTime.Now;
        }
        public IEnumerable<Club> ClubList
        {
            get => _clubList;
            set => Set(ref _clubList, value);
        }
        public IEnumerable<League> LeagueList
        {
            get => _leagueList;
            set => Set(ref _leagueList, value);
        }
        public CreatingOwnerViewModel OwnerViewModel
        {
            get => _ownerViewModel;
        }
        public string ClubName
        {
            get => _clubName;
            set => Set(ref _clubName, value);
        }
        public int PosInUEFA
        {
            get => _posInUEFA;
            set => Set(ref _posInUEFA, value);
        }
        public double Cost
        {
            get => _cost;
            set => Set(ref _cost, value);
        }
        public int AmountTrophies
        {
            get => _amountTrophies;
            set => Set(ref _amountTrophies, value);
        }
        public DateTime? DateFoundation
        {
            get => _dateFoundation;
            set => Set(ref _dateFoundation, value);
            
        }
        public int PosInLeague
        {
            get => _posInLeague;
            set => Set(ref _posInLeague, value);
        }
        public string Schema
        {
            get => _schema;
            set {
                if (value.Length < 11)
                {
                    Set(ref _schema, value);
                }
                else
                {
                    Set(ref _schema, value[38 ..]);
                }
            }
        }
        public League? League
        {
            get => _league;
            set => Set(ref _league, value);
        }
        public List<object> GetClubData()
        {
            if (DateFoundation == null)
            {
                return new List<object>() {
                    true
                };
            }
            return new List<object>()
            {
                false, ClubName, PosInLeague, DateFoundation, PosInUEFA, Cost, AmountTrophies, Schema, League.Name,
            };
        }
        public List<object> GetOwnerData()
        {
            return OwnerViewModel.GetOwnerData();
        }
    }
}
