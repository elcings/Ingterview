using IdentityService.Api.Core.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Infrastructure.Data
{
    public static  class DbInit
    {
        public static void Seed(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<IdentityContext>();

  
                var roles = new List<Role> {

                      new Role{Id=Guid.NewGuid(), Name="User"},
                      new Role{ Id=Guid.NewGuid(),Name="Admin"}

                    };
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(roles);

                }
                context.SaveChanges();

                //if (!context.Users.Any())
                //{
                //    var users = new List<User> {
                //     new User
                //     {
                //         Id=new Guid("cb46418d-d658-4407-a0c9-6b65a998d5f2"),
                //         Email="elcinaliyevgs@gmail.com",
                //         CreatedDate=DateTime.Now,
                //         PasswordHash="SgyE6AUD8L7vsh4UxKezJ3bIZRn4GcyzEdRO09K4RHxh5+AuPiX1Jm5a/zx3GNNZahSPLq2yhAJJOgeZNjUT9A==",
                //         PasswordSalt="xN8pwnCnz1HoIkm/XUNEhb8mvxPPkthTHqK/4LrPdL0zhMQKeHqcDxnamltV/99dAWqm+KiOPg7Nq4FBgFJNkwqy15TLnZAFyVmH2UO0WAX8t0SDktOWyi03YFbZeh2yIfyrrcdi2avkNLEU5Ar1EQbvHk5jVOUxzohdrEuiiDE=",
                //         Status=0,
                //         Firstname="Elcin",
                //         Lastname="Aliyev",
                //         Roles=roles
                //     },

                //    };
                //    context.Users.AddRange(users);

                //}


                //context.SaveChanges();

            }
        }

    }
}
