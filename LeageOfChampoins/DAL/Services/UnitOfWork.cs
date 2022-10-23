using BLL.Entities;
using DAL.Absractions.Interfaces;
using DAL.DataBase;
using DAL.Services;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly DALContext _context = new DALContext();
        private IDbContextTransaction _transaction;
        private GenericRepository<League> _leagueRepository;
        private GenericRepository<Player> _playerRepository;
        private GenericRepository<Club> _clubRepository;
        private GenericRepository<Couch> _couchRepository;
        private GenericRepository<Owner> _ownerRepository;
        private GenericRepository<Account> _accountRepository;
        private bool _disposed = false;

        public GenericRepository<Couch> CouchRepository
        {
            get
            {
                if (_couchRepository == null)
                {
                    this._couchRepository = new GenericRepository<Couch>(_context);
                }
                return this._couchRepository;
            }
        }
        public GenericRepository<Club> ClubRepository
        {
            get 
            {
                if (_clubRepository == null)
                {
                    this._clubRepository = new GenericRepository<Club>(_context);
                }
                return this._clubRepository; 
            }
        }
        public GenericRepository<League> LeagueRepository
        {
            get 
            {
                if (_leagueRepository == null)
                {
                    this._leagueRepository = new GenericRepository<League>(_context);
                }
                return this._leagueRepository; 
            }
        }
        public GenericRepository<Player> PlayerRepository
        {
            get
            {
                if (_playerRepository == null)
                {
                    this._playerRepository = new GenericRepository<Player>(_context);
                }
                return this._playerRepository;
            }
        }
        public GenericRepository<Owner> OwnerRepository
        {
            get
            {
                if (_ownerRepository == null)
                {
                    this._ownerRepository = new GenericRepository<Owner>(_context);
                }
                return this._ownerRepository;
            }
        }
        public GenericRepository<Account> AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    this._accountRepository = new GenericRepository<Account>(_context);
                }
                return this._accountRepository;
            }
        }
        public void CreateTransaction()
        {
            _transaction = _context.Database.BeginTransaction();                  
        }
        public void RollBack()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }

            else
            {
                _transaction = _context.Database.BeginTransaction();
                _transaction.Rollback();
                _transaction.Dispose();
            }

        }
        public void Commit()
        {
            _transaction.Commit();
        }
        private async Task Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }

            this._disposed = true;
        }
        public async void Dispose()
        {
            await this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        ~UnitOfWork()
        {
            _ = Dispose(false);
        }
    }
}
