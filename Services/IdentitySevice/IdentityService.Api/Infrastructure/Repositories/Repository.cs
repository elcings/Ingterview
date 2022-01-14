using IdentityService.Api.Core.Domain.Entities;
using IdentityService.Api.Core.Domain.Repositories;
using IdentityService.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.Repositories
{
    public  class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private IdentityContext _ctx;
        private DbSet<TEntity> _dbSet;
        public Repository(IdentityContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TEntity>();
        }

        public async Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return await Task.Run<IQueryable<TEntity>>(()=> {

                IQueryable<TEntity> query = _dbSet;
                if (filter != null)
                    query = query.Where(filter);

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                if (orderBy != null)
                    query = orderBy(query);
                return query;


            });
            
        }


        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            var result = await Task.Run<IQueryable<TEntity>>(() => {
                return _dbSet.OrderBy(x => x.Id);
            });
            return result;
        }

        public async Task<TEntity> GetByIdAsync(Guid Id,string includeProperties="")
        {
            IQueryable<TEntity> query = _dbSet;


            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync();
        }


        public async Task<Guid> CreateAsync(TEntity obj)
        {
            var result = await _dbSet.AddAsync(obj);
            await _ctx.SaveChangesAsync();
            return obj.Id;
        }

        public async Task UpdateAsync(TEntity obj)
        {
            var entity = await _dbSet.FindAsync(obj.Id);
            var entry = _ctx.Entry(entity);
            entry.CurrentValues.SetValues(obj);
            await _ctx.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid Id)
        {
            var data = await _dbSet.FindAsync(Id);
            _ctx.Remove(data);
            await _ctx.SaveChangesAsync();
        }

    }
}

