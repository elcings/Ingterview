﻿using Interview.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common.Interfaces
{
   public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
