using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.IntegrationEvents
{
   public class OrderStartedIntegrationEvent:IntegrationEvent
    {
        public string UserName { get; set; }
        public OrderStartedIntegrationEvent(string username) => UserName = username;
    }
}
