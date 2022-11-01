using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface ICouchService
    {
        Task<int> CreateCouch(Couch player);
        //Task DeleteCouch(Couch player);
        Task UpdateCouch(Couch player);

        public Task<IEnumerable<Couch>> GetCouches();
        public Task<bool> CouchExists(Expression<Func<Couch, bool>> predicate);
        public Task<IEnumerable<Couch>> GetCouch(Expression<Func<Couch, bool>> predicate);
    }
}
