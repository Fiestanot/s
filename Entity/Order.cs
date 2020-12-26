using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Order : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string CustomerId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
