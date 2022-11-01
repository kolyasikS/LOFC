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
    public class CharacteristicsFieldPlrService : ICharacteristicsFieldPlrService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public CharacteristicsFieldPlrService()
        {

        }

        public async Task<int> CreateCharacteristics(CharacteristicsFieldPlr characteristics)
        {

            _unitOfWork.CreateTransaction();

            try
            {
                await _unitOfWork.CharacteristicsFieldPlrRepository.Insert(characteristics);

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

        public async Task UpdateCharacteristics(CharacteristicsFieldPlr characteristics)
        {
            _unitOfWork.CreateTransaction();

            try
            {
                _unitOfWork.CharacteristicsFieldPlrRepository.Update(characteristics);

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

        public async Task<IEnumerable<CharacteristicsFieldPlr>> GetAllCharacteristics()
        {
            IEnumerable<CharacteristicsFieldPlr> characteristics = null;

            _unitOfWork.CreateTransaction();
            try
            {
                characteristics = await _unitOfWork.CharacteristicsFieldPlrRepository.Get();

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

        public async Task<IEnumerable<CharacteristicsFieldPlr>> GetCharacteristics(Expression<Func<CharacteristicsFieldPlr, bool>> predicate)
        {
            IEnumerable<CharacteristicsFieldPlr> characteristics = null;

            _unitOfWork.CreateTransaction();

            try
            {
                characteristics = await _unitOfWork.CharacteristicsFieldPlrRepository.Get(predicate);

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

       /* public async Task<bool> ClubExists(Expression<Func<Club, bool>> predicate)
        {
            IEnumerable<Club> club = null;

            _unitOfWork.CreateTransaction();

            try
            {
                club = await _unitOfWork.ClubRepository.Get(predicate);

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

            return club.FirstOrDefault() != null;

        }*/
    }
}
