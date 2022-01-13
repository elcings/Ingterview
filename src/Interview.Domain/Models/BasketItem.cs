using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Models
{
    public class BasketItem
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
 
    }
}
