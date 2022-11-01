using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface ICharacteristicsGoalkeeperService
    {
        Task<int> CreateCharacteristics(CharacteristicsGoalkeeper characteristics);
        //Task DeleteClub(Club club);
        Task UpdateCharacteristics(CharacteristicsGoalkeeper characteristics);

        public Task<IEnumerable<CharacteristicsGoalkeeper>> GetAllCharacteristics();
        //public Task<bool> ClubExists(Expression<Func<Club, bool>> predicate);
        public Task<IEnumerable<CharacteristicsGoalkeeper>> GetCharacteristics(Expression<Func<CharacteristicsGoalkeeper, bool>> predicate);
    }
}
