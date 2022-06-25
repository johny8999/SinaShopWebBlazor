using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SinaShop.Application.Contract.PresentationDTO.ViewModels
{
    public class vmListAccessLevelModel
{
        public string Id { get; set; }

        [Display(Name = nameof(Name))]
        public string Name { get; set; }

        [Display(Name = nameof(UserCount))]
        public int UserCount { get; set; }
    }
}
