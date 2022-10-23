using BLL.Entities;
using DAL.Absractions.Interfaces;
using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace DAL.Services
{
    public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity : IdKey
    {
        private readonly DALContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository(DALContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            try
            {
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList(); // can be error
                }
                else
                {
                    return query.ToList();
                    //return await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<TEntity>();
            }
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Insert(TEntity obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public async Task InsertRange(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Delete(TEntity obj)
        {
            if (_context.Entry(obj).State == EntityState.Detached)
            {
                _dbSet.Attach(obj);
            }

            _dbSet.Remove(obj);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task DeleteById(int id)
        {
            var obj = await _dbSet.FindAsync(id);

            this.Delete(obj);
        }

        public void Update(TEntity obj)
        {
            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
