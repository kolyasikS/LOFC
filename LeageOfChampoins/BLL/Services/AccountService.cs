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
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public AccountService()
        {

        }

        public async Task<int> CreateAccount(Account account)
        {

            _unitOfWork.CreateTransaction();

            try
            {
                await _unitOfWork.AccountRepository.Insert(account);

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
            return account.Id;
        }

        public async Task DeleteAccount(Account account)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.AccountRepository.Delete(account);

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

        public async Task UpdateAccount(Account account)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.AccountRepository.Update(account);

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
        public async Task<IEnumerable<Account>> GetAccount(Expression<Func<Account, bool>> predicate)
        {
            IEnumerable<Account> account = null;

            _unitOfWork.CreateTransaction();

            try
            {
                account = await _unitOfWork.AccountRepository.Get(predicate);

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

            return account;
        }

        public async Task<bool> AccountExists(Expression<Func<Account, bool>> predicate)
        {
            IEnumerable<Account> account = null;

            _unitOfWork.CreateTransaction();

            try
            {
                account = await _unitOfWork.AccountRepository.Get(predicate);

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

            return account.FirstOrDefault() != null;

        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            IEnumerable<Account> accounts = null;

            _unitOfWork.CreateTransaction();
            try
            {
                accounts = await _unitOfWork.AccountRepository.Get();

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

            return accounts;
        }
    }
}
