using firstproject.Models;
using System.Linq.Expressions;

namespace firstproject.Repository.IRepository
{
    public interface IVillaRepository
    {
        Task CreateAsync(Villa entity);
        Task RemoveAsync(Villa entity);
        Task SaveAsync();
        Task <List<Villa>> GetAllAsync(Expression<Func<Villa,bool>> filter = null);
        Task<Villa> GetAsync(Expression<Func<Villa,bool>> filter = null,bool tracked=true);
        Task UpdateAsync(Villa entity);
    }
}
