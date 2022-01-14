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
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private IMapper _mapper;
        private IActionInvoker _actionInvoker;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository,
            IMapper mapper, IActionInvoker actionInvoker)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _actionInvoker = actionInvoker;
        }

        public async Task<ActionResult<Guid>> AddUser(UserDto dto)
        {
            return await _actionInvoker.InvokeAsync<Guid>(async () =>
            {
                var entity = _mapper.Map<User>(dto);
                return await _userRepository.CreateAsync(entity);
            });

        }

        public async Task<ActionResult> AddUserRole(Guid userId, string roleName = "user")
        {
            return await _actionInvoker.InvokeAsync(async () =>
            {
                 await _userRepository.AddRole(userId,roleName);
            });
        }
    }
}
