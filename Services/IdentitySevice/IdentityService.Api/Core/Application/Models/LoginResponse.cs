using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Core.Application.Models
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string  Token { get; set; }
    }
}
