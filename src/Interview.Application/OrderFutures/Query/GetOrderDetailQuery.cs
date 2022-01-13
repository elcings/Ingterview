using AutoMapper;
using Interview.Application.Common.Models;
using Interview.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.OrderFutures.Query
{
    public class GetOrderDetailQuery:IRequest<ActionResult<OrderDetailViewModel>>
    {

        public Guid OrderId { get; set; }
        
    }

    public class GetOrderDetailQueryHandler : IRequestHandler<GetOrderDetailQuery, ActionResult<OrderDetailViewModel>>
    {
        private IOrderRepository _repository;
        private IMapper _mapper;

        public GetOrderDetailQueryHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActionResult<OrderDetailViewModel>> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdFilter(request.OrderId,  "OrderItems,OrderStatus");

            var model = _mapper.Map<OrderDetailViewModel>(entity);

            return new ActionResult<OrderDetailViewModel>().Succeed(model);
        }
    }
}
