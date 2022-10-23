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
    public class OwnerService : IOwnerService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public OwnerService()
        {

        }

        public async Task CreateOwner(Owner owner)
        {

            _unitOfWork.CreateTransaction();

            try
            {
                await _unitOfWork.OwnerRepository.Insert(owner);

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

        public async Task DeleteOwner(Owner owner)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.OwnerRepository.Delete(owner);

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

        public async Task UpdateOwner(Owner owner)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.OwnerRepository.Update(owner);

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

        public async Task<IEnumerable<Owner>> GetOwners(Func<IQueryable<Owner>, IOrderedQueryable<Owner>>? orderBy = null)
        {
            IEnumerable<Owner> owners = null;

            _unitOfWork.CreateTransaction();
            try
            {
                owners = await _unitOfWork.OwnerRepository.Get(null, orderBy);

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

            return owners;
        }

        public async Task<IEnumerable<Owner>> GetOwner(Expression<Func<Owner, bool>> predicate)
        {
            IEnumerable<Owner> owner = null;

            _unitOfWork.CreateTransaction();

            try
            {
                owner = await _unitOfWork.OwnerRepository.Get(predicate);

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

            return owner;
        }

        public async Task<bool> OwnerExists(Expression<Func<Owner, bool>> predicate)
        {
            IEnumerable<Owner> owner = null;

            _unitOfWork.CreateTransaction();

            try
            {
                owner = await _unitOfWork.OwnerRepository.Get(predicate);

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

            return owner.FirstOrDefault() != null;

        }
    }
}
