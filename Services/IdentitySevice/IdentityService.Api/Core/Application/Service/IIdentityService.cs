using IdentityService.Api.Core.Application.Common;
using IdentityService.Api.Core.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Core.Application.Service
{
    public interface IIdentityService
    {
        Task<ActionResult<LoginResponse>> ValidateUser(LoginRequest request);
        Task<ActionResult<Guid>> Register(RegisterModel model);
    }
}
