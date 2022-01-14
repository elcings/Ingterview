using IdentityService.Api.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Core.Domain.Repositories
{
   public interface IRoleRepository: IRepository<Role>
    {
        List<Role> GetUserRoles(Guid userId);
    }
}
