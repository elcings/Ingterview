using Interview.Application.Common.Models;
using Interview.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.CarError.Queries
{
    public class GetErrorByIdQueryHandler : IRequestHandler<GetErrorByIdQuery, Response>
    {
        private IErrorRepository _repository;
        public GetErrorByIdQueryHandler(IErrorRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response> Handle(GetErrorByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetById(request.Id);
            return new Response() { Data = response ,StatusCode=System.Net.HttpStatusCode.OK};
        }
    }
}
