using IdentityService.Api.Core.Application.Common;
using IdentityService.Api.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Core.Application.Service
{
    public interface IUserService
    {
        Task<ActionResult<Guid>> AddUser(UserDto dto);
        Task<ActionResult> AddUserRole(Guid userId ,string roleName="user");
    }
       
}
