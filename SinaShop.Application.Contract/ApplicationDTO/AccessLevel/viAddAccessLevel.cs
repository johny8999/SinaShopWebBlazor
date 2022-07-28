using FrameWork.Common.DataAnnotations.Strings;
using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SinaShop.Application.Contract.ApplicationDTO.AccessLevel
{
    public class viAddAccessLevel
    {
        [DisplayName(nameof(Name))]
        [RequiredString]
        [MaxLengthString(100)]
        public string Name { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
