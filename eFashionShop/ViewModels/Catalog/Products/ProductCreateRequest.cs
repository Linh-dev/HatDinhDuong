﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eFashionShop.ViewModels.Catalog.Products
{
    public class ProductCreateRequest
    {
        [Required(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string Customer { set; get; }
        public bool IsFeatured { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}