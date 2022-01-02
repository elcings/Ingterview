using Interview.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAll();
        Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetById(Guid Id);
        Task<TEntity> Create(TEntity obj);
        Task CreateList(List<TEntity> obj);
        Task<TEntity> Update(TEntity obj);
        Task Remove(Guid Id);
        Task RemoveAll(List<TEntity> obj);
    }
}
