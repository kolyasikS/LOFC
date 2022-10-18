using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Absractions.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : IdKey
    {
        public Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        public Task Insert(TEntity obj);
        public void Delete(TEntity obj);
        public Task DeleteById(int id);
        public void Update(TEntity obj);
        public Task InsertRange(IEnumerable<TEntity> entities);
        public void DeleteRange(IEnumerable<TEntity> entities);
    }
}
