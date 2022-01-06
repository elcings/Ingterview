using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common.Models
{
    public class DinResponse
    {
        public string Message { get; set; }
        public List<Din> Value { get; set; }
       
    }

    public class Din {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
