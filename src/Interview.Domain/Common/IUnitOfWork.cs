using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Domain.Common
{
    public interface IUnitOfWork
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
