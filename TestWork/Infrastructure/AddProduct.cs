using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.Infrastructure
{
    public class AddProduct
    {
        public Guid ProductTypeId { get; set; }

        public string NameProduct { get; set; }

        public decimal PriceProduct { get; set; }

        public int CountProduct { get; set; }
    }
}
