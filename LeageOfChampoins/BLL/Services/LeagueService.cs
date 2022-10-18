using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces; 
using BLL.Entities;
using DAL.Absractions.Interfaces;
using DAL.Services;

namespace BLL.Services
{
    public class LeagueService : ILeagueService
    {
        //private readonly IGenericRepository<League> _genericRepository;

        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        //public LeagueService(IGenericRepository<League> repository)
        //{
        //    _genericRepository = repository;
        //}
        public LeagueService()
        {

        }

        public async Task CreateLeague(League league)
        {

            _unitOfWork.CreateTransaction();

            try
            {
                await _unitOfWork.LeagueRepository.Insert(league);

                await _unitOfWork.SaveAsync();

                _unitOfWork.Commit();

            }
            catch (Exception e)
            {
                try
                {
                    _unitOfWork.RollBack();
                }
                catch (Exception e1)
                {

                }
            }

        }

        public async Task DeleteLeague(League league)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.LeagueRepository.Delete(league);

                await _unitOfWork.SaveAsync();

                _unitOfWork.Commit();

            }
            catch (Exception e)
            {
                try
                {
                    _unitOfWork.RollBack();
                }
                catch (Exception e1)
                {

                }
            }
        }

        public async Task UpdateLeague(League league)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.LeagueRepository.Update(league);

                await _unitOfWork.SaveAsync();

                _unitOfWork.Commit();

            }
            catch (Exception e)
            {
                try
                {
                    _unitOfWork.RollBack();
                }
                catch (Exception e1)
                {

                }
            }
        }

        public async Task<IEnumerable<League>> GetLeagues()
        {
            IEnumerable<League> leagues = null;
            
            try
            {
                leagues = await _unitOfWork.LeagueRepository.Get();
            }
            catch (Exception e)
            {
            }

            return leagues;
        }

        public async Task<IEnumerable<League>> GetLeague(Expression<Func<League, bool>> predicate)
        {
            IEnumerable<League> league = null;

            try
            {
                league = await _unitOfWork.LeagueRepository.Get(predicate);

            }
            catch (Exception e)
            {

            }

            return league;
        }

        public async Task<bool> LeagueExists(Expression<Func<League, bool>> predicate)
        {
            IEnumerable<League> league = null;

            _unitOfWork.CreateTransaction();

            try
            {
                league = await _unitOfWork.LeagueRepository.Get(predicate);

                // await _unitOfWork.SaveAsync();

                _unitOfWork.Commit();

            }
            catch (Exception e)
            {
                try
                {
                    _unitOfWork.RollBack();
                }
                catch (Exception e1)
                {

                }
            }

            return league.FirstOrDefault() != null;

        }
    }
}
