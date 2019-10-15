﻿using MVCProjectBase.Core.Domain.Entity;
using System.Data.Entity.ModelConfiguration;

namespace MVCProjectBase.Data.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("Role");
            HasKey(x => x.Id);
            Property(x => x.RoleName).HasMaxLength(100);           
        }
    }
}
