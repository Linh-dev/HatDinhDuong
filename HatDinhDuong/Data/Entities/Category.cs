﻿using HatDinhDuong.Commom;

namespace HatDinhDuong.Data.Entities
{
    public class Category
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int? SortOrder { set; get; }
        public bool IsShowOnHome { set; get; }
        public int? ParentId { set; get; }
        public Status Status { set; get; }
        public string ImagePublishId { set; get; }
        public string ImageUrl { set; get; }
        public List<ProductInCategory> ProductInCategories { get; set; }
    }
}
