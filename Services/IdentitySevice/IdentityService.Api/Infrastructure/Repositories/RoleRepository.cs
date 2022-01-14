using IdentityService.Api.Core.Domain.Entities;
using IdentityService.Api.Core.Domain.Repositories;
using IdentityService.Api.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        IdentityContext _ctx;
        public RoleRepository(IdentityContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public List<Role> GetUserRoles(Guid userId)
        {
            var user = _ctx.Users.Where(x => x.Id == userId).FirstOrDefault();

            return user.Roles.ToList();
        }
    }
}
