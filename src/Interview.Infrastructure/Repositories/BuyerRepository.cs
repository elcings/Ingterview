using Interview.Domain.AggregateModels.Buyer;
using Interview.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Repositories
{
    public class BuyerRepository : BaseRepository<Buyer>, IBuyerRepository
    {
        public BuyerRepository(OrderDbContext ctx) : base(ctx)
        {
        }
    }
}
