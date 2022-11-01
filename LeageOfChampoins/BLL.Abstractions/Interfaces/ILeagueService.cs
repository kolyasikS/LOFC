using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface ILeagueService
    {
        Task CreateLeague(League league);
        Task DeleteLeague(League league);
        Task UpdateLeague(League league);

        public Task<IEnumerable<League>> GetLeagues(Expression<Func<League, bool>>? filter = null, Func<IQueryable<League>, IOrderedQueryable<League>>? orderBy = null);
        public Task<bool> LeagueExists(Expression<Func<League, bool>> predicate);
        public Task<IEnumerable<League>> GetLeague(Expression<Func<League, bool>> predicate);
    }
}
