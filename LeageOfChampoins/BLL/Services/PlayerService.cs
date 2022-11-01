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
    public class PlayerService : IPlayerService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        
        public PlayerService()
        {

        }

        public async Task CreatePlayer(Player player)
        {

            _unitOfWork.CreateTransaction();

            try
            {
                await _unitOfWork.PlayerRepository.Insert(player);

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

        public async Task DeletePlayer(Player player)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.PlayerRepository.Delete(player);

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

        public async Task UpdatePlayer(Player player)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.PlayerRepository.Update(player);

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

        public async Task<IEnumerable<Player>> GetPlayers(Expression<Func<Player, bool>>? filter = null, Func<IQueryable<Player>, IOrderedQueryable<Player>>? orderBy = null)
        {
            IEnumerable<Player> players = null;
            
            try
            {
                players = await _unitOfWork.PlayerRepository.Get(filter, orderBy);
            }
            catch (Exception e)
            {
            }

            return players;
        }

        public async Task<IEnumerable<Player>> GetPlayer(Expression<Func<Player, bool>> predicate)
        {
            IEnumerable<Player> player = null;

            try
            {
                player = await _unitOfWork.PlayerRepository.Get(predicate);

                _unitOfWork.Commit();

            }
            catch (Exception e)
            {
               
            }

            return player;
        }

        public async Task<bool> PlayerExists(Expression<Func<Player, bool>> predicate)
        {
            IEnumerable<Player> player = null;

            _unitOfWork.CreateTransaction();

            try
            {
                player = await _unitOfWork.PlayerRepository.Get(predicate);

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

            return player.FirstOrDefault() != null;

        }
    }
}
