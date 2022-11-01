using BLL.Abstractions.Interfaces;
using BLL.Entities;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CouchService : ICouchService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public CouchService()
        {

        }

        public async Task<int> CreateCouch(Couch couch)
        {

            _unitOfWork.CreateTransaction();

            try
            {
                await _unitOfWork.CouchRepository.Insert(couch);

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
            return couch.Id;

        }

/*        public async Task DeleteCouch(Couch club)
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
        }*/

        public async Task UpdateCouch(Couch couch)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.CouchRepository.Update(couch);

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

        public async Task<IEnumerable<Couch>> GetCouches()
        {
            IEnumerable<Couch> couches = null;

            _unitOfWork.CreateTransaction();
            try
            {
                couches = await _unitOfWork.CouchRepository.Get();

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

            return couches;
        }

        public async Task<IEnumerable<Couch>> GetCouch(Expression<Func<Couch, bool>> predicate)
        {
            IEnumerable<Couch> couch = null;

            _unitOfWork.CreateTransaction();

            try
            {
                couch = await _unitOfWork.CouchRepository.Get(predicate);

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

            return couch;
        }

        public async Task<bool> CouchExists(Expression<Func<Couch, bool>> predicate)
        {
            IEnumerable<Couch> couch = null;

            _unitOfWork.CreateTransaction();

            try
            {
                couch = await _unitOfWork.CouchRepository.Get(predicate);

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

            return couch.FirstOrDefault() != null;

        }
    }
}
