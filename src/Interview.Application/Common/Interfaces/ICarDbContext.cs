using Interview.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.Common.Interfaces
{
    public interface ICarDbContext
    {
        DbSet<Distance> Distances { get; }
        DbSet<FuelLevel> FuelLevels { get; }
        DbSet<Error> Errors { get;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
