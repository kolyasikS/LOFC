using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IAccountService
    {
        Task<int> CreateAccount(Account account);
        Task DeleteAccount(Account account);
        Task UpdateAccount(Account account);

        //public Task<IEnumerable<Account>> GetClubs();
        public Task<bool> AccountExists(Expression<Func<Account, bool>> predicate);
        public Task<IEnumerable<Club>> GetAccount(Expression<Func<Account, bool>> predicate);
    }
}
