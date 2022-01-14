using IdentityService.Api.Core.Domain.Entities;
using IdentityService.Api.Core.Domain.Repositories;
using IdentityService.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        IdentityContext _ctx;
        public UserRepository(IdentityContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task AddRole(Guid userId, string roleName)
        {
            var user = await _ctx.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            var role = await _ctx.Roles.Where(x => x.Name == roleName).FirstOrDefaultAsync();
            user.Roles.Add(role);
           await base.UpdateAsync(user);
        }
    }
}
