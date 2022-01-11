using Interview.Domain.Common;
using Interview.Domain.Enum;
using Interview.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Entities
{
    public class Distance: BaseEntity,IAggregateRoot
    {
        public long distance { get; set; }
        public Colour Colour { get; set; } = Colour.White;
        public virtual List<TodoItem> TodoItems { get; set; } =new List<TodoItem>();
    }
}
