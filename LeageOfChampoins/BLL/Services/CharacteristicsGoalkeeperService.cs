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
    public class CharacteristicsGoalkeeperService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public CharacteristicsGoalkeeperService()
        {

        }

        public async Task<int> CreateCharacteristics(CharacteristicsGoalkeeper characteristics)
        {

            _unitOfWork.CreateTransaction();

            try
            {
                await _unitOfWork.CharacteristicsGoalkeeperRepository.Insert(characteristics);

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
            return characteristics.Id;

        }

        /* public async Task DeleteClub(Club characteristics)
         {
             _unitOfWork.CreateTransaction();

             try
             {
                 _unitOfWork.CharacteristicsFieldPlrRepository.Delete(characteristics);

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
         }*/

        public async Task UpdateCharacteristics(CharacteristicsGoalkeeper characteristics)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.CharacteristicsGoalkeeperRepository.Update(characteristics);

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

        public async Task<IEnumerable<CharacteristicsGoalkeeper>> GetAllCharacteristics()
        {
            IEnumerable<CharacteristicsGoalkeeper> characteristics = null;

            _unitOfWork.CreateTransaction();
            try
            {
                characteristics = await _unitOfWork.CharacteristicsGoalkeeperRepository.Get();

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

            return characteristics;
        }

        public async Task<IEnumerable<CharacteristicsGoalkeeper>> GetCharacteristics(Expression<Func<CharacteristicsGoalkeeper, bool>> predicate)
        {
            IEnumerable<CharacteristicsGoalkeeper> characteristics = null;

            _unitOfWork.CreateTransaction();

            try
            {
                characteristics = await _unitOfWork.CharacteristicsGoalkeeperRepository.Get(predicate);

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

            return characteristics;
        }

    }
}
