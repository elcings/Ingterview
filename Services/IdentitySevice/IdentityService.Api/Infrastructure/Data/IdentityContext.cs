using IdentityService.Api.Core.Application.Service;
using IdentityService.Api.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.Data
{
    public class IdentityContext:DbContext
    {
        public const string DEFAULT_SCHEMA = "identity";
        ISessionService _sessionService;
        public IdentityContext(DbContextOptions<IdentityContext> options, ISessionService sessionService) : base(options) {
            _sessionService = sessionService;

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userName = _sessionService.UserName;
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userName;
                        entry.Entity.Created = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}
