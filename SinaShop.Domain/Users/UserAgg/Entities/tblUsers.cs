using FrameWork.Domain;
using Microsoft.AspNetCore.Identity;
using SinaShop.Domain.Users.AccessLevelAgg.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SinaShop.Domain.Users.UserAgg.Entities
{
    public class tblUsers : IdentityUser<Guid>, IEntity
    {
        public Guid AccessLevelId { get; set; }
        public string Fullname { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }

         
        public virtual tblAccessLevel tblAccessLevel { get; set; }
    }
}
