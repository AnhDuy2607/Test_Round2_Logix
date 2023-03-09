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
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();
        }
    }
}
