using Interview.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Entities
{
    public class TodoItem:BaseEntity
    {
        public string Name { get; set; }
        public virtual Distance Distance { get; set; }
    }
}
