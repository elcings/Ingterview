using Interview.Application.CarError.Command;
using Interview.Application.CarError.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private readonly ILogger<WeatherForecastController> _logger;
        IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
           var result =await _mediator.Send(new CreateErrorCommand() { Description=""}); 
            return Ok(result);
        }
    }
}
