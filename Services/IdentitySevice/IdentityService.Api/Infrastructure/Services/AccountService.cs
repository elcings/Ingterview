using AutoMapper;
using IdentityService.Api.Core.Application.Common;
using IdentityService.Api.Core.Application.Common.Exceptions;
using IdentityService.Api.Core.Application.Models;
using IdentityService.Api.Core.Application.Service;
using IdentityService.Api.Core.Domain.Entities;
using IdentityService.Api.Core.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.Services
{
    public class AccountService:IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private IMapper _mapper;
        private IConfiguration _configuration;
        private IActionInvoker _actionInvoker;

        public AccountService(IUserRepository userRepository, IRoleRepository roleRepository, 
            IMapper mapper,IConfiguration configuration,IActionInvoker actionInvoker)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _configuration = configuration;
            _actionInvoker = actionInvoker;
        }

        public async Task<ActionResult<LoginResponse>> ValidateUser(LoginRequest request)
        {
            var result = await _actionInvoker.InvokeAsync<LoginResponse>(async () => {
                if (string.IsNullOrEmpty(request.Email))
                    throw new GeneralValidateException("email_is_empty");
                if (string.IsNullOrEmpty(request.Password))
                    throw new GeneralValidateException("password_is_empty");

                var user = (await _userRepository.Query(x => x.Email == request.Email)).FirstOrDefault(); ;
                if (user == null)
                    throw new GeneralValidateException("user_or_password_is_not_correct");

                if (!VerifyPasswordHash(request.Password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt)))
                {

                    throw new GeneralValidateException("user_or_password_is_not_correct");
                }
                var token = generateJwtToken(user.Email, user.Id);

                return new LoginResponse
                {
                    UserId = user.Id,
                    Token = token,
                };

            }, "AccountService.ValidateUser", false);

            return result;

           

        }


        public async Task<ActionResult<Guid>> Register(RegisterModel model)
        {
            var result = await _actionInvoker.InvokeAsync<Guid>(async () => {
                if ((await _userRepository.Query(x => x.Email == model.Email)).Any())
                    throw new GeneralValidateException("email_already_taken");
                if (string.IsNullOrWhiteSpace(model.Password))
                    throw new GeneralValidateException("password_is_null");

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

                var userId = await _userRepository.CreateAsync(new User
                {
                    Email = model.Email,
                    Firstname = model.FirstName,
                    Lastname = model.LastName,
                    Created = DateTime.Now,
                    PasswordHash = Convert.ToBase64String(passwordHash),
                    PasswordSalt = Convert.ToBase64String(passwordSalt)
                });

                await _userRepository.AddRole(userId, "User");

                return userId;

            }, "AccountService.Register", false);

            return result;

           
        }


        #region Helper

        private string generateJwtToken(string userName, Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AuthConfig:Secret"].ToString());
            var claims = new List<Claim>
                {   new Claim("Id",userId.ToString()),
                    new Claim("UserName",userName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims: claims),
                Expires = DateTime.UtcNow.AddYears(70),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

      

        #endregion
    }
}
