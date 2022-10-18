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
        private GenericRepository<League> leagueRepository;
        private GenericRepository<Player> playerRepository;
        private GenericRepository<Club> clubRepository;
        private GenericRepository<Couch> couchRepository;
        private bool _disposed = false;

        public GenericRepository<Couch> CouchRepository
        {
            get
            {
                if (couchRepository == null)
                {
                    this.couchRepository = new GenericRepository<Couch>(_context);
                }
                return this.couchRepository;
            }
        }
        public GenericRepository<Club> ClubRepository
        {
            get 
            {
                if (clubRepository == null)
                {
                    this.clubRepository = new GenericRepository<Club>(_context);
                }
                return this.clubRepository; 
            }
        }
        public GenericRepository<League> LeagueRepository
        {
            get 
            {
                if (leagueRepository == null)
                {
                    this.leagueRepository = new GenericRepository<League>(_context);
                }
                return this.leagueRepository; 
            }
        }
        public GenericRepository<Player> PlayerRepository
        {
            get
            {
                if (playerRepository == null)
                {
                    this.playerRepository = new GenericRepository<Player>(_context);
                }
                return this.playerRepository;
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
