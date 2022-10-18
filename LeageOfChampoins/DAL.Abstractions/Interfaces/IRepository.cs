using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Absractions.Interfaces
{
    internal interface IRepository<T> where T : IdKey
    {
        Task CreateObjectAsync(T obj);
        Task<IEnumerable<T>> GetAllAsync(Type type);
        Task UpdateObjectAsync(T obj);
        Task DeleteObjectAsync(T obj);
    }
}
