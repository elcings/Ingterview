using IdentityService.Api.Core.Application.Service;
using IdentityService.Api.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.Services
{
    public class SessionService:ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserName
        {
            get
            {
                var fullname = _httpContextAccessor.HttpContext.User?.Identity?.GetUserName();
                return fullname;
            }
        }
    }
}
