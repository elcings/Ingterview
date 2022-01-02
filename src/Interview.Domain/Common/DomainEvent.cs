using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTime.Now;
        }
        public bool IsPublished { get; set; }
        public DateTime DateOccurred { get; protected set; } = DateTime.Now;
    }
}
