using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace SinaShop.Application.Contract.ApplicationDTO.UsersDto
{
    public class InpLgIn
    {
        [Display(Name = nameof(UserId))]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }
        [Display(Name = nameof(Password))]
        [RequiredString]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
