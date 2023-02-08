using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Enums
{
    public enum CultureType
    {
        None = 0,
        [Display(Name = "en-US")]
        US = 1,
        [Display(Name = "tr-TR")]
        TR = 2,

    }
}
