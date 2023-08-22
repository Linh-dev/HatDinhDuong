using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eFashionShop.Data.Entities
{
    public class PricePerProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MassId { get; set; }
        public int ProductTypeId { get; set; }
        public double Price { get; set; }
    }
}
