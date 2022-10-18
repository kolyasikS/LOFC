using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.Abstractions.Interfaces
{
    public interface IPlayerService
    {
        Task CreatePlayer(Player player);
        Task DeletePlayer(Player player);
        Task UpdatePlayer(Player player);

        public Task<IEnumerable<Player>> GetPlayers();
        public Task<bool> PlayerExists(Expression<Func<Player, bool>> predicate);
        public Task<IEnumerable<Player>> GetPlayer(Expression<Func<Player, bool>> predicate);
    }
}
