using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;

namespace SinaShop.Application.Contract.ApplicationDTO.AccessLevel
{
    public class InputDeleteAccessLevel
    {
        [Display(Name = nameof(Id))]
        [RequiredString]
        [GUID]
        public string Id { get; set; }
    }
}
