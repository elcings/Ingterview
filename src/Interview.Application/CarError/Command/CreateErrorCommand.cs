using AutoMapper;
using Interview.Domain.Entities;
using Interview.Domain.Events;
using Interview.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.CarError.Command
{
   public  class CreateErrorCommand:IRequest<Guid>
    {
        public string Description { get; set; }
        public string Mail { get; set; }
    }

    public class CreateErrorCommandHandler : IRequestHandler<CreateErrorCommand, Guid>
    {
        private readonly IErrorRepository _repository;
        private IMapper _mapper;

        public CreateErrorCommandHandler(IErrorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
     
        public async Task<Guid> Handle(CreateErrorCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Error>(request);
            entity.DomainEvents.Add(new ErrorCreatedEvent(entity));

            var result = await _repository.Create(entity);

            return result.Id;

        }
    }
}
