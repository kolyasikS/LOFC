using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstractions.Interfaces
{
    public interface ICharacteristicsFieldPlrService
    {
        Task<int> CreateCharacteristics(CharacteristicsFieldPlr characteristics);
        //Task DeleteClub(Club club);
        Task UpdateCharacteristics(CharacteristicsFieldPlr characteristics);

        public Task<IEnumerable<CharacteristicsFieldPlr>> GetAllCharacteristics();
        //public Task<bool> ClubExists(Expression<Func<Club, bool>> predicate);
        public Task<IEnumerable<CharacteristicsFieldPlr>> GetCharacteristics(Expression<Func<CharacteristicsFieldPlr, bool>> predicate);
    }
}
