﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SinaShop.Domain.Users.AccessLevelAgg.Entities;
using SinaShop.Infrastructure.EfCore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaShop.Infrastructure.EfCore.Mapping.AccessLevels
{
    public class tblAccessLevelConfiguration : IEntityTypeConfiguration<tblAccessLevel>, IEntityConf
    {
        public void Configure(EntityTypeBuilder<tblAccessLevel> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).IsRequired().HasMaxLength(450);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);

        }
    }
}
