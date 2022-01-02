using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.CarStop.Events
{
    public class CarStoppedCompletedIntegrationEvent:IntegrationEvent
    {
        public string Message { get; set; }
        public CarStoppedCompletedIntegrationEvent(string message)
        {
            Message = message;
        }
    }
}
