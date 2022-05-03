﻿using FrameWork.Domain;
using SinaShop.Domain.Users.RoleAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Domain.Users.AccessLevelAgg.Entities
{
    public class tblAccessLevelRoles:IEntity
    {
        public Guid Id { get; set; }
        public Guid AccessLevelId { get; set; }
        public Guid RoleId { get; set; }

        public tblRoles tblRoles { get; set; }
        public tblAccessLevel tblAccessLevel { get; set; }
    }
}
