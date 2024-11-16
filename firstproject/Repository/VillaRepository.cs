using firstproject.Data;
using firstproject.Models;
using firstproject.Repository.IRepository;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace firstproject.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        async Task IVillaRepository.CreateAsync(Villa entity)
        {   entity.CreatedDate = DateTime.Now;
            _db.Villas.AddAsync(entity);
           await _db.SaveChangesAsync();
        }

        async Task<Villa> IVillaRepository.GetAsync(Expression<Func<Villa,bool>> filter=null, bool tracked=true)
        {
            IQueryable<Villa> query = _db.Villas;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query=query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        async Task<List<Villa>> IVillaRepository.GetAllAsync(Expression<Func<Villa,bool>> filter)
        {
            IQueryable<Villa> query = _db.Villas;
            if(filter != null)
            {
                query.Where(filter);
            }
            return await query.ToListAsync();
        }

        async Task IVillaRepository.RemoveAsync(Villa entity)
        {
            _db.Villas.Remove(entity);
            await _db.SaveChangesAsync();
        }

        async Task IVillaRepository.SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        async Task IVillaRepository.UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
           _db.Villas.Update(entity);
           await _db.SaveChangesAsync();
        }
    }
}
