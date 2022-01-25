using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Core.Application.Service
{
    public interface ISessionService
    {
         string UserName { get; }
    }
}
