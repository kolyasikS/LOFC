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
    /// Interaction logic for LeaguesWindow.xaml
    /// </summary>
    public partial class LeaguesWindow : Window
    {
        private LeagueService _leagueService = new();
        private LeagueViewModel _leagueViewModel;
        public LeaguesWindow()
        {
            InitializeComponent();

            var leagues = _leagueService.GetLeagues();
            _leagueViewModel = new LeagueViewModel(leagues);
            this.DataContext = _leagueViewModel;
        }

        private void GetTopLeagues(object sender, RoutedEventArgs e)
        {
            PlayerService playerService = new();
            ClubService clubService = new();

            var players = playerService.GetPlayers(player => player.Age > 30).Result.ToList();
            var listClubsId = players.Select(p =>  p.ClubId);
            
            var clubs = clubService.GetClubs(club => listClubsId.Contains(club.Id)).Result.ToList();
            var clubsGG = clubs.Select(c => new {c.Id, c.LeagueId, stats = players.Where(p => p.ClubId == c.Id).Average(p => (double)p.Goals / p.Games)});

            var leagues = _leagueService.GetLeagues(l => clubsGG.Select(c => c.LeagueId).Contains(l.Id)).Result.ToList();
            var result = clubsGG.Join(leagues, c => c.LeagueId, l => l.Id, (c, l) => new { clubsGG = c, leagues = l })
                .GroupBy(commonTable => commonTable.leagues.Id)
                .Select(ct => new { league = ct.Select(l => l.leagues).Distinct().First(), stats = ct.Average(ct => ct.clubsGG.stats)})
                .OrderByDescending(l => l.stats)
                .ToList();
            _leagueViewModel.FilterLeagueList = result.OrderByDescending(r => r.stats).Select(r => r.league);
        }
    }
}
