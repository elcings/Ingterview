using AutoMapper;
using Interview.Application.Common.Mapping;
using Interview.Domain.Entities;
using Interview.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview.Application.Fuel.Command
{
    public class CreateFuelLevelCommand : IRequest<Guid>
    {
        public int Level { get; set; }
    }
    public class CreateFuelLevelCommandHandler : IRequestHandler<CreateFuelLevelCommand, Guid>
    {
        private readonly IFuelLevelRepository _repository;
        private IMapper _mapper;

        public CreateFuelLevelCommandHandler(IFuelLevelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateFuelLevelCommand request, CancellationToken cancellationToken)
        {

            var entity = _mapper.Map<FuelLevel>(request);

            var result = await _repository.Create(entity);

            return result.Id;
        }
    }
}
