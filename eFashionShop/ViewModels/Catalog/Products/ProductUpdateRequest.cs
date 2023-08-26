using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace eFashionShop.ViewModels.Catalog.Products
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string Trademark { set; get; }
        public string ProductOrigin { set; get; }
        public string Ingredient { set; get; }
        public string Expiry { set; get; }
        public string BlogDescription { set; get; }
        public bool IsFeatured { get; set; }
    }
}