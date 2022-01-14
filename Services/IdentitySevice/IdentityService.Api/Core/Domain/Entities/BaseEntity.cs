using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Api.Core.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
