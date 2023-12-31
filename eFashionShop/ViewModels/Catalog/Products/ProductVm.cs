﻿using eFashionShop.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace eFashionShop.ViewModels.Catalog.Products
{
    public class ProductVm
    {
        public int Id { set; get; }
        public DateTime DateCreated { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public bool IsFeatured { get; set; }
        public string Trademark { set; get; }
        public string ProductOrigin { set; get; }
        public string Ingredient { set; get; }
        public string Expiry { set; get; }
        public string BlogDescription { set; get; }
        public string ThumbnailImage { get; set; }
        public List<int> Categories { get; set; } = new List<int>();
    }
}