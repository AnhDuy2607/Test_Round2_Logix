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
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.SaltPwd).IsRequired();
            builder.Property(x => x.HashPwd).IsRequired();
            builder.Property(x => x.Token).IsRequired(false);
            builder.Property(x => x.ExpiredTime).IsRequired(false);
            builder.Property(x => x.UserId).IsRequired(false);

            builder.HasIndex(x => x.Username).IsUnique();
        }
    }
}
