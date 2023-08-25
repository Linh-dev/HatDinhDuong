using eFashionShop.Data.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eFashionShop.ViewModels.Catalog.Categories
{
    public class CategoryCreateVm
    {
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Name { set; get; }
        public bool IsShowOnHome { set; get; }
        public bool IsMainCategory { set; get; }
        public int? ParentId { set; get; }
        public int? ImageId { get; set; }
        public IFormFile File { get; set; }
        public Status Status { set; get; }
    }
}
