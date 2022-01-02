using Interview.Application.Common.Exceptions;
using Interview.Domain.Entities;
using Interview.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.Distances.Command
{
    public class DeleteDistanceCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteDistanceCommand>
    {
        private readonly IDistanceRepository _repository;

        public DeleteTodoItemCommandHandler(IDistanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteDistanceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Distance), request.Id);
            }

            await _repository.Remove(request.Id);
            return Unit.Value;
        }
    }
}
