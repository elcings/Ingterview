using Interview.Application.Common.Interfaces;
using Interview.Domain.Common;
using Interview.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private OrderDbContext _ctx;
        private DbSet<TEntity> _dbSet;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _ctx;
            }
        }

        public BaseRepository(OrderDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TEntity>();
        }


        public async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.OrderBy(x => x.Id).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid Id)
        {
            return await _dbSet.FirstOrDefaultAsync(x=>x.Id==Id);
        }

        public async Task Remove(Guid Id)
        {
            var data = await _dbSet.FindAsync(Id);
            _ctx.Remove(data);
        }

        public async Task<bool> Create(TEntity obj)
        {
            var result = await _dbSet.AddAsync(obj);
            return true;
            
        }


        public async Task<bool> Update(TEntity obj)
        {
            var entity =await _dbSet.FindAsync(obj.Id);
            var entry = _ctx.Entry(entity);
            entry.CurrentValues.SetValues(obj);
            return true;
        }



        public async Task<IQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return await Task.Run<IQueryable<TEntity>>(() => {

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

        public async Task CreateList(List<TEntity> obj)
        {
            await _dbSet.AddRangeAsync(obj);
        }

        public async Task RemoveAll(List<TEntity> obj)
        {
           await Task.Run(() => _ctx.RemoveRange(obj)) ;
        }

        public virtual async Task<TEntity> GetByIdFilter(Guid Id, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
           

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
           
            return await query.FirstOrDefaultAsync();
        }
    }
}
