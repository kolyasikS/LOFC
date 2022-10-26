using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface IClubService
    {
        Task<int> CreateClub(Club club);
        Task DeleteClub(Club club);
        Task UpdateClub(Club club);

        public Task<IEnumerable<Club>> GetClubs(Expression<Func<Club, bool>>? filter = null, Func<IQueryable<Club>,IOrderedQueryable<Club>>? orderBy = null);
        public Task<bool> ClubExists(Expression<Func<Club, bool>> predicate);
        public Task<IEnumerable<Club>> GetClub(Expression<Func<Club, bool>> predicate);
    }
}
