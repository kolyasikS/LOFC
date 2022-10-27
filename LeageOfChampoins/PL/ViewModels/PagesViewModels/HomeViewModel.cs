using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL.ViewModels.PagesViewModels
{
    public class HomeViewModel : ViewModel
    {
        private IEnumerable<League> _leagueList;

        private string _clubName;
        private string _clubInfoName;
        private int _posInUEFA;
        private int _posInLeague;
        private double _cost;
        private int _amountTrophies;

        private DateTime? _dateFoundation;
        private string _dateFoundationLabel;

        private string _schema;
        private League? _league;
        private string _dateLabelFoundation;

        public HomeViewModel(Club club, IEnumerable<League> leagues)
        {
            ClubName = "Barcelona";

            LeagueList = leagues;
            ClubInfoName = club.Name;
            PosInUEFA = club.PosUEFARatingClubs;
            Cost = club.Cost;
            AmountTrophies = club.AmountTrophies;
            DateFoundation = club.DateFoundation.ToDateTime(new TimeOnly(0));
            PosInLeague = club.posInLeague;
            Schema = club.Schema;
            League = new List<League>(LeagueList).Find(item => item.Id == club.LeagueId);
        }
        public IEnumerable<League> LeagueList
        {
            get => _leagueList;
            set => Set(ref _leagueList, value);
        }
        public string DateLabelFoundation
        {
            get => _dateLabelFoundation;
            set => Set(ref _dateLabelFoundation, value);
        }
        public string ClubName
        {
            get => _clubName;
            set => Set(ref _clubName, value);
        }
        public string ClubInfoName
        {
            get => _clubInfoName;
            set => Set(ref _clubInfoName, value);
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
            set
            {
                DateFoundationLabel = value?.ToString("d", CultureInfo.GetCultureInfo("de-De")) ?? string.Empty;
                Set(ref _dateFoundation, value);
            }
        }
        public string DateFoundationLabel
        {
            get => _dateFoundationLabel;
            set => Set(ref _dateFoundationLabel, value);
        }
        public int PosInLeague
        {
            get => _posInLeague;
            set => Set(ref _posInLeague, value);
        }
        public string Schema
        {
            get => _schema;
            set
            {
                if (value.Length < 11)
                {
                    Set(ref _schema, value);
                }
                else
                {
                    Set(ref _schema, value[38..]);
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
       
    }
}
