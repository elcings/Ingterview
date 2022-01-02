using Interview.Domain.Common;
using Interview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Events
{
    public class ErrorCreatedEvent : DomainEvent
    {
        public ErrorCreatedEvent(Error err)
        {
            Error = err;
        }

        public Error Error { get; }
    }
}
