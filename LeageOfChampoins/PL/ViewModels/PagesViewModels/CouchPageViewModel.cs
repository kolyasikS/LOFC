using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels.PagesViewModels
{
    public class CouchPageViewModel : ViewModel
    {
        private string _couchName;
        private string _couchSurname;
        //private string _clubInfoName;
        private string _schema;

        private int _amountTrophies;
        private int _age;
        private int _games;

        private string _pointsPerGame;
        private string _salary;

        private DateTime? _dateExpirContract;
        private string _dateExpirContractLabel;

        public CouchPageViewModel(Couch couch)
        {
            CouchName = couch.Name;
            CouchSurname = couch.Surname;
            Schema = couch.MainSchema;
            AmountTrophies = couch.AmountTrophies;
            Age = couch.Age;
            Games = couch.Games;
            PointsPerGame = couch.PointsPerGame.ToString();
            Salary = couch.Salary.ToString();
            DateExpirContract = couch.DateExpirContract.ToDateTime(new TimeOnly(0));
        }
        public void SetCouch(Couch couch)
        {
            CouchName = couch.Name;
            CouchSurname = couch.Surname;
            Schema = couch.MainSchema;
            AmountTrophies = couch.AmountTrophies;
            Age = couch.Age;
            Games = couch.Games;
            PointsPerGame = couch.PointsPerGame.ToString();
            Salary = couch.Salary.ToString();
            DateExpirContract = couch.DateExpirContract.ToDateTime(new TimeOnly(0));
        }
        public string DateExpirContractLabel
        {
            get => _dateExpirContractLabel;
            set => Set(ref _dateExpirContractLabel, value);
        }
        public string CouchName
        {
            get => _couchName;
            set => Set(ref _couchName, value);
        }
        public string CouchSurname
        {
            get => _couchSurname;
            set => Set(ref _couchSurname, value);
        }
        public int AmountTrophies
        {
            get => _amountTrophies;
            set => Set(ref _amountTrophies, value);
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
        public string PointsPerGame
        {
            get => _pointsPerGame;
            set
            {
                if (double.TryParse(value, out _) || value == string.Empty)
                {
                    Set(ref _pointsPerGame, value);
                }
            }
        }
        public string Salary
        {
            get => _salary;
            set
            {
                if (double.TryParse(value, out _) || value == string.Empty)
                {
                    Set(ref _salary, value);
                }
            }
        }
        public DateTime? DateExpirContract
        {
            get => _dateExpirContract;
            set
            {
                DateExpirContractLabel = value?.ToString("d", CultureInfo.GetCultureInfo("de-De")) ?? string.Empty;
                Set(ref _dateExpirContract, value);
            }
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
    }
}
