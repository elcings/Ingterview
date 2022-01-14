using IdentityService.Api.Core.Application.Service;
using m= IdentityService.Api.Core.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] m.LoginRequest model) {
           var result= await _identityService.ValidateUser(model);
            return Ok(result);
        }

      //  [Authorize]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] m.RegisterModel model)
        {
            var result = await _identityService.Register(model);
            return Ok(result);
        }

    }
}
