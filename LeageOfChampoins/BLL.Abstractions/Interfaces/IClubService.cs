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
        Task CreateClub(Club club);
        Task DeleteClub(Club club);
        Task UpdateClub(Club club);

        public Task<IEnumerable<Club>> GetClubs();
        public Task<bool> ClubExists(Expression<Func<Club, bool>> predicate);
        public Task<IEnumerable<Club>> GetClub(Expression<Func<Club, bool>> predicate);
    }
}
