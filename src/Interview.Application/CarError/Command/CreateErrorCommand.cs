using Ardalis.GuardClauses;
using AutoMapper;
using Interview.Application.Common;
using Interview.Application.Common.Models;
using Interview.Application.Validations;
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
   public  class CreateErrorCommand:IRequest<Response>
    {
        public string Description { get; set; }
    }
    //Validation

    public class Validator : IValidationHandler<CreateErrorCommand>
    {
        public async  Task<ValidationResult> Validate(CreateErrorCommand request)
        {
            var result= Guard.Against.IsNullOrEmpty(request.Description, nameof(request.Description), message: "CreateErrorCommand.Description must not be null or empty");
            return result;
        }
    }
    public class CreateErrorCommandHandler : IRequestHandler<CreateErrorCommand, Response>
    {
        private readonly IErrorRepository _repository;
        private IMapper _mapper;

        public CreateErrorCommandHandler(IErrorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
     
        public async Task<Response> Handle(CreateErrorCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Error>(request);
           // entity.DomainEvents.Add(new ErrorCreatedEvent(entity));

            var result = await _repository.Create(entity);

            return new Response() { Data=result};

        }
    }
}
