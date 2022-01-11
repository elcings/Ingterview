using Interview.Domain.Common;
using Interview.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Interview.Domain.Entities
{
    public class Error:BaseEntity, IHasDomainEvent, IAggregateRoot
    {
        public string Description { get; set; }

        private bool _done;
        public bool Done
        {
            get => _done;
            set
            {
                if (value == true && _done == false)
                {
                    DomainEvents.Add(new ErrorCreatedEvent(this));
                }

                _done = value;
            }
        }


        [JsonIgnore]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
