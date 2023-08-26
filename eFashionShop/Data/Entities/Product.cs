using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eFashionShop.Data.Entities
{
    public class Product
    {
        public int Id { set; get; }
        public string Name { set; get; }
        /// <summary>
        /// mo ta
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// mo ta chi tiet
        /// </summary>
        public string Details { set; get; }
        /// <summary>
        /// thuong hieu
        /// </summary>
        public string Trademark { set; get; }
        /// <summary>
        /// nguon goc
        /// </summary>
        public string ProductOrigin { set; get; }
        /// <summary>
        /// thanh phan
        /// </summary>
        public string Ingredient { set; get; }  
        /// <summary>
        /// han su sung
        /// </summary>
        public string Expiry { set; get; }  
        /// <summary>
        /// blog mo ta su dung va cong dung cua sp
        /// </summary>
        public string BlogDescription { set; get; }  
        public DateTime DateCreated { set; get; }
        /// <summary>
        /// noi bat
        /// </summary>
        public bool IsFeatured { get; set; }
        public List<ProductInCategory> ProductInCategories { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
