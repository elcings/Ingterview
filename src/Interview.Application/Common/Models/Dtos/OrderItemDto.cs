using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common.Models.Dtos
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Unit { get; set; }

    }
}
