using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Application.Common.Models
{
    public class OrderDetailViewModel
    {
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
       
    }
    public class OrderItemModel {
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public int Unit { get; set; }
        public double UnitPrice { get; set; }

    }
}
