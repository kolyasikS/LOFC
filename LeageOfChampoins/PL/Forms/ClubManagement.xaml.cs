using BLL.Entities;
using BLL.Services;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LOFC.PL.Forms
{
    /// <summary>
    /// Interaction logic for ClubManagement.xaml
    /// </summary>
    public partial class ClubManagement : Window
    {
        private ClubManagementViewModel _clubManagementViewModel;
        private ClubService _clubService = new();
        private OwnerService _ownerService = new();
        private PlayerService _playerService = new();
        private CouchService _couchService = new();
        private AccountService _accountService = new();
        private CharacteristicsFieldPlrService _characteristicsFieldPlrService = new();
        private CharacteristicsGoalkeeperService _characteristicsGoalkeeperService = new();

        private bool isModified = false;
        public ClubManagement(Club club, Owner owner)
        {
            InitializeComponent();

            _clubManagementViewModel = new ClubManagementViewModel(club);
            this.DataContext = _clubManagementViewModel;
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
