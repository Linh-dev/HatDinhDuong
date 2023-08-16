using System.ComponentModel.DataAnnotations;

namespace HatDinhDuong.Commom
{
    public enum Status
    {
        [Display(Name = "Không hoạt động")]
        InActive,
        [Display(Name = "Hoạt động")]
        Active
    }
}
