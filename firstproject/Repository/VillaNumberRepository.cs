using firstproject.Data;
using firstproject.Models;
using firstproject.Repository.IRepository;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace firstproject.Repository
{
    public class VillaNumberRepository : IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        async Task IVillaNumberRepository.CreateAsync(VillaNumber entity)
        {   entity.CreatedDate = DateTime.Now;
            _db.VillaNumbers.AddAsync(entity);
           await _db.SaveChangesAsync();
        }

        async Task<VillaNumber> IVillaNumberRepository.GetAsync(Expression<Func<VillaNumber, bool>> filter=null, bool tracked=true)
        {
            IQueryable<VillaNumber> query = _db.VillaNumbers;
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

        async Task<List<VillaNumber>> IVillaNumberRepository.GetAllAsync(Expression<Func<VillaNumber, bool>> filter)
        {
            IQueryable<VillaNumber> query = _db.VillaNumbers;
            if(filter != null)
            {
                query.Where(filter);
            }
            return await query.ToListAsync();
        }

        async Task IVillaNumberRepository.RemoveAsync(VillaNumber entity)
        {
            _db.VillaNumbers.Remove(entity);
            await _db.SaveChangesAsync();
        }

        async Task IVillaNumberRepository.SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        async Task IVillaNumberRepository.UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
           _db.VillaNumbers.Update(entity);
           await _db.SaveChangesAsync();
        }
    }
}
