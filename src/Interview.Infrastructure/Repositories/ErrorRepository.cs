using Interview.Domain.Entities;
using Interview.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Repositories
{
    public class ErrorRepository : BaseRepository<Error>, IErrorRepository
    {
        public ErrorRepository(CarDbContext ctx) : base(ctx)
        {
        }
    }
}
