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
    public class LikeHistoryConfiguration : IEntityTypeConfiguration<LikeHistory>
    {
        public void Configure(EntityTypeBuilder<LikeHistory> builder)
        {
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.MovieId).IsRequired();
        }
    }
}
