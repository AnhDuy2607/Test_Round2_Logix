using BE.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infrastructure.DataContext.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Firstname).IsRequired();
            builder.Property(x => x.Lastname).IsRequired();
            builder.Property(x => x.AccountId).IsRequired(false);
        }
    }
}
