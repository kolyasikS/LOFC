using BLL.Abstractions.Interfaces;
using BLL.Entities;
using DAL.Absractions.Interfaces;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ClubService: IClubService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public ClubService()
        {

        }

        public async Task<int> CreateClub(Club club)
        {

            _unitOfWork.CreateTransaction();

            try
            {
                await _unitOfWork.ClubRepository.Insert(club);

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
            return club.Id;

        }

        public async Task DeleteClub(Club club)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.ClubRepository.Delete(club);

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

        public async Task UpdateClub(Club club)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.ClubRepository.Update(club);

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

        public async Task<IEnumerable<Club>> GetClubs(Expression<Func<Club, bool>>? filter = null, Func<IQueryable<Club>,IOrderedQueryable<Club>>? orderBy = null)
        {
            IEnumerable<Club> clubs = null;

            _unitOfWork.CreateTransaction();
            try
            {
                clubs = await _unitOfWork.ClubRepository.Get(filter, orderBy);

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

            return clubs;
        }

        public async Task<IEnumerable<Club>> GetClub(Expression<Func<Club, bool>> predicate)
        {
            IEnumerable<Club> club = null;

            _unitOfWork.CreateTransaction();

            try
            {
                club = await _unitOfWork.ClubRepository.Get(predicate);

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

            return club;
        }

        public async Task<bool> ClubExists(Expression<Func<Club, bool>> predicate)
        {
            IEnumerable<Club> club = null;

            _unitOfWork.CreateTransaction();

            try
            {
                club = await _unitOfWork.ClubRepository.Get(predicate);

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

            return club.FirstOrDefault() != null;

        }
    }
}
