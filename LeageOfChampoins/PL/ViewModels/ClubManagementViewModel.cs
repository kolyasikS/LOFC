using BLL.Entities;
using GalaSoft.MvvmLight.Command;
using LOFC.PL.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.ViewModels
{
    public class ClubManagementViewModel : ViewModel
    {
        private Page _homePage;
        private Page _ownerPage;
        private Page _couchPage;
        private Page _playersPage;

        private Page _currentPage;

        private double _frameOpacity;
        private bool isLoading = false;
        public ClubManagementViewModel(Club club, Owner owner, List<Player>? players)
        {
            _homePage    = new HomePage(club);
            _ownerPage   = new OwnerPage(owner);
            _couchPage   = new CouchPage(club.CouchId, UpdateCouch);
            _playersPage = new PlayersPage(players, club.Id);

            FrameOpacity = 1;
            CurrentPage = _homePage;
        }
        public void UpdateCouch(int CouchId)
        {
            ((HomePage)_homePage).UpdateCouch(CouchId);
        }
        public double FrameOpacity
        {
            get => _frameOpacity;
            set => Set(ref _frameOpacity, value);
        }
        public Page CurrentPage
        {
            get => _currentPage;
            set 
            {
                Set(ref _currentPage, value);
            }
        }

        public ICommand? toHomeClick
        {
            get
            {
                return new RelayCommand(() => SlowOpacity(_homePage));
            }
        }
        public ICommand? toOwnerClick
        {
            get
            {
                return new RelayCommand(() => SlowOpacity(_ownerPage));
            }
        }
        public ICommand? toCouchClick
        {
            get
            {
                return new RelayCommand(() => SlowOpacity(_couchPage));
            }
        }
        public ICommand? toPlayersClick
        {
            get
            {
                return new RelayCommand(() => SlowOpacity(_playersPage));
            }
        }

        private async void SlowOpacity(Page page)
        {
            if (isLoading || CurrentPage == page)
            {
                return;
            }
            else
            {
                isLoading = true;
            }
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0; i -= 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(20);
                }
                CurrentPage = page;
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(20);
                }
            });
            isLoading = false;
        }
    }
}
