using Interview.Domain.AggregateModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Repositories
{
    public interface IOrderRepository:IRepository<Order>
    {
    }
}
