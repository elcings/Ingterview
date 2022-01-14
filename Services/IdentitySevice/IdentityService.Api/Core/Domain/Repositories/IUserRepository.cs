using IdentityService.Api.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Core.Domain.Repositories
{
   public interface IUserRepository: IRepository<User>
    {
        Task AddRole(Guid userId, string roleName);
    }

}
