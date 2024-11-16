using firstproject.Models;
using System.Linq.Expressions;

namespace firstproject.Repository.IRepository
{
    public interface IVillaNumberRepository
    {
        Task CreateAsync(VillaNumber entity);
        Task RemoveAsync(VillaNumber entity);
        Task SaveAsync();
        Task <List<VillaNumber>> GetAllAsync(Expression<Func<VillaNumber, bool>> filter = null);
        Task<VillaNumber> GetAsync(Expression<Func<VillaNumber, bool>> filter = null,bool tracked=true);
        Task UpdateAsync(VillaNumber entity);
    }
}
