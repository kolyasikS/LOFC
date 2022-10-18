using BLL.Entities;
using DAL.Absractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class Repository<T> : IRepository<T> where T : IdKey
    {
        public Task CreateObjectAsync(T obj)
        {
            throw new NotImplementedException();
        }
        public Task DeleteObjectAsync(T obj)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<T>> GetAllAsync(Type type)
        {
            throw new NotImplementedException();
        }
        public Task UpdateObjectAsync(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
