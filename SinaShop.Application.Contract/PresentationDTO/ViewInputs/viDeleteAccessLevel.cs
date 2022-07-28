using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using FrameWork.Common.DataAnnotations.Strings;

namespace SinaShop.Application.Contract.PresentationDTO.ViewInputs
{
    public class viDeleteAccessLevel
    {
        [Display(Name = nameof(Id))]
        [RequiredString]
        [GUID]
        public string Id { get; set; }
    }
}
