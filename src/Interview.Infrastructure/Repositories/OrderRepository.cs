using Interview.Domain.AggregateModels.Orders;
using Interview.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        OrderDbContext _ctx;
        public OrderRepository(OrderDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public override async Task<Order> GetByIdFilter(Guid Id, string includeProperties = "")
        {
            var entity=await base.GetByIdFilter(Id, includeProperties);
            if (entity == null)
            {
                entity = _ctx.Orders.Local.FirstOrDefault(x => x.Id == Id);
            }
            return entity;
        }
    }
}
