using Interview.Application.OrderFutures.Command;
using Interview.Application.OrderFutures.Query;
using Interview.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        IMediator _mediator;

        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetOrderDetailQuery() { OrderId = Guid.NewGuid() });
            return Ok(result);
        }

        //[HttpGet("post")]
        //public async Task<IActionResult> Create()
        //{
        //    var result = await _mediator.Send(new CreateOrderCommand(new List<BasketItem>() {
        //     new BasketItem{PictureUrl="test7",ProductId=Guid.NewGuid(),ProductName="TestName3",Quantity=5,UnitPrice=12 },
        //     new BasketItem{PictureUrl="test8",ProductId=Guid.NewGuid(),ProductName="TestName2",Quantity=4,UnitPrice=18 },
        //    }, "Elcin2", "Baku1", "Baku1", "Azerbaycan1", "Asiq Ali 2", "1002", "1111111111", "Elcin Aliyev", DateTime.Now, "125", 3));
        //    return Ok(result);
        //}
    }
}
