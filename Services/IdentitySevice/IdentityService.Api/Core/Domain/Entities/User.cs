using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Core.Domain.Entities
{
    public class User:BaseEntity
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
