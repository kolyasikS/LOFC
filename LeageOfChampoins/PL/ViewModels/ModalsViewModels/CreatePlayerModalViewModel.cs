using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels.ModalsViewModels
{
    public class CreatePlayerModalViewModel : ViewModel
    {
        private string _playerName;
        private string _playerSurname;

        private int? _individualAwards;
        private int? _age;
        private int? _games;
        private int? _number;
        private int? _goals;
        private int? _assists;

        private string _salary;
        private string _position;
        private string _cost;

        public CreatePlayerModalViewModel()
        {
            SetDefault();
        }
        public void SetDefault()
        {
            _playerName = String.Empty;
            _playerSurname = String.Empty;
            _individualAwards = null;
            _age = null;
            _games = null;
            _number = null;
            _goals = null;
            _assists = null;
            _salary = String.Empty;
            _position = String.Empty;
            _cost = String.Empty;
        }
        public Player CreatePlayer(int ClubId)
        {
#pragma warning disable CS8629 // Nullable value type may be null.
            var player = new Player()
            {
                Name = PlayerName,
                Surname = PlayerSurname,
                IndividualAwards = IndividualAwards.Value,
                Age = Age.Value,
                Games = Games.Value,
                Number = Number.Value,
                Goals = Goals.Value,
                Assists = Assists.Value,
                Salary = Convert.ToDouble(Salary),
                Position = Position,
                Cost = Convert.ToDouble(Cost),
                ClubId = ClubId,
            };
            return player;
#pragma warning restore CS8629 // Nullable value type may be null.
        }
        public string PlayerName
        {
            get => _playerName;
            set => Set(ref _playerName, value);
        }
        public string PlayerSurname
        {
            get => _playerSurname;
            set => Set(ref _playerSurname, value);
        }
        public int? IndividualAwards
        {
            get => _individualAwards;
            set => Set(ref _individualAwards, value);
        }
        public int? Age
        {
            get => _age;
            set => Set(ref _age, value);
        }
        public int? Games
        {
            get => _games;
            set => Set(ref _games, value);
        }
        public int? Number
        {
            get => _number;
            set => Set(ref _number, value);
        }
        public int? Goals
        {
            get => _goals;
            set => Set(ref _goals, value);
        }
        public int? Assists
        {
            get => _assists;
            set => Set(ref _assists, value);
        }
        public string Position
        {
            get => _position;
            set => Set(ref _position, value);
        }
        public string Cost
        {
            get => _cost;
            set
            {
                if (double.TryParse(value, out _) || value == string.Empty)
                {
                    Set(ref _cost, value);
                }
            }
        }
        public string Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                if (double.TryParse(value, out _) || value == string.Empty)
                {
                    Set(ref _salary, value);
                }
            }
        }
    }
}
