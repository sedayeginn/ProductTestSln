using System;
using System.Collections.Generic;

#nullable disable

namespace DB.Entities
{
    public partial class Product
    {
        public int Idproduct { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
