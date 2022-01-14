using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Core.Domain.Entities
{
    public class Role:BaseEntity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
