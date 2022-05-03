using SinaShop.WebApp.Common.DataAnnotations.Strings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Application.Contract.ApplicationDTO.AccessLevel
{
    public class InpGetUserRollesByAccessLevel
    {
        [Display (Name =nameof(AccessLevelId))]
        [RequiredString]
        [StringLength(150)]
        public string AccessLevelId { get; set; }
    }
}
