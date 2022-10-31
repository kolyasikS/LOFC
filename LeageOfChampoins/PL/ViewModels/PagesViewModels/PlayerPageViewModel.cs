using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModels.PagesViewModels
{
    public class PlayerPageViewModel : ViewModel
    {
        private List<Player> _playerList;
        private Player _selectedPlayer;

        private string _playerName;
        private string _playerSurname;
        private string _playerFullName;

        private int _individualAwards;
        private int _age;
        private int _games;
        private int _number;
        private int _goals;
        private int _assists;

        private string _salary;
        private string _position;
        private string _cost;

        public PlayerPageViewModel(List<Player> players)
        {
            PlayerList = players;
            SelectedPlayer = players[0];
            SetPlayer(SelectedPlayer);
        }
        public void SetPlayer(Player player)
        {
            PlayerName = player.Name;
            PlayerSurname = player.Surname;
            IndividualAwards = player.IndividualAwards;
            Age = player.Age;
            Games = player.Games;
            Number = player.Number;
            Goals = player.Goals;
            Assists = player.Assists;
            Salary = player.Salary.ToString();
            Position = player.Position;
            Cost = player.Cost.ToString();
            
            PlayerFullName = PlayerName + " " + PlayerSurname;

        }
        public string PlayerFullName
        {
            get => _playerFullName;
            set => Set(ref _playerFullName, value);
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
        public int IndividualAwards
        {
            get => _individualAwards;
            set => Set(ref _individualAwards, value);
        }
        public int Age
        {
            get => _age;
            set => Set(ref _age, value);
        }
        public int Games
        {
            get => _games;
            set => Set(ref _games, value);
        }
        public int Number
        {
            get => _number;
            set => Set(ref _number, value);
        }
        public int Goals
        {
            get => _goals;
            set => Set(ref _goals, value);
        }
        public int Assists
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

        public List<Player> PlayerList
        {
            get => _playerList;
            set => Set(ref _playerList, value);
        }
        public Player SelectedPlayer
        {
            get => _selectedPlayer;
            set
            {
                Set(ref _selectedPlayer, value);
                SetPlayer(_selectedPlayer);
            }
        }
    }
}
