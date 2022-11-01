using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IOwnerService
    {
        Task CreateOwner(Owner owner);
        Task DeleteOwner(Owner owner);
        Task UpdateOwner(Owner owner);

        public Task<IEnumerable<Owner>> GetOwners(Func<IQueryable<Owner>, IOrderedQueryable<Owner>>? orderBy = null);
        public Task<bool> OwnerExists(Expression<Func<Owner, bool>> predicate);
        public Task<IEnumerable<Owner>> GetOwner(Expression<Func<Owner, bool>> predicate);
    }
}
