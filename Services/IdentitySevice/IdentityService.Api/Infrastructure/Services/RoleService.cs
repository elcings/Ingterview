using AutoMapper;
using IdentityService.Api.Core.Application.Common;
using IdentityService.Api.Core.Application.Dtos;
using IdentityService.Api.Core.Application.Service;
using IdentityService.Api.Core.Domain.Entities;
using IdentityService.Api.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private IMapper _mapper;
        private IActionInvoker _actionInvoker;

        public RoleService(IRoleRepository roleRepository,
            IMapper mapper, IActionInvoker actionInvoker)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _actionInvoker = actionInvoker;
        }

        public async Task<ActionResult> AddRole(RoleDto dto)
        {
            return await _actionInvoker.InvokeAsync<Guid>(async () =>
            {
                var entity = _mapper.Map<Role>(dto);
                return await _roleRepository.CreateAsync(entity);
            });

        }
        
    }
}
