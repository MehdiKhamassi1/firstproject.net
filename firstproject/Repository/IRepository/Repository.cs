using firstproject.Data;
using firstproject.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace firstproject.Repository.IRepository
{
    public class Repository<T> : IRepository<T> where T :class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        async Task IRepository<T>.CreateAsync(T entity)
        {
            dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        async Task<T> IRepository<T>.GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        async Task<List<T>> IRepository<T>.GetAllAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query.Where(filter);
            }
            return await query.ToListAsync();
        }

        async Task IRepository<T>.RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        async Task IRepository<T>.SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

       /* async Task IRepository<T>.UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await _db.SaveChangesAsync();
        }*/
    }
}
