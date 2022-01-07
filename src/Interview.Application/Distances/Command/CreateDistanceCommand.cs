using Enum=Interview.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interview.Domain.Repositories;
using AutoMapper;
using System.Threading;
using Interview.Domain.Entities;
using Interview.Application.Common.Models;

namespace Interview.Application.Distances.Command
{
    public class CreateDistanceCommand:IRequest<Guid>
    {
        public long Distance { get; set; }
        public string Colour { get; set; }
        public List<ToDoItemDTO> ToDoItems { get; set; }
    }

    public class CreateDistanceCommandHandler : IRequestHandler<CreateDistanceCommand, Guid>
    {
        private readonly IDistanceRepository _repository;
        private IMapper _mapper;

        public CreateDistanceCommandHandler(IDistanceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateDistanceCommand request, CancellationToken cancellationToken)
        {

            var entity = _mapper.Map<Distance>(request);

            var result = await _repository.Create(entity);

            return result.Id;
        }
    }
}
