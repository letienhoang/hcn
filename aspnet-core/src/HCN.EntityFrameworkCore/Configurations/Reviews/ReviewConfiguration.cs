using HCN.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Reviews
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "Reviews");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Expense)
                .HasColumnType("decimal(19,4)")
                .IsRequired();

            builder.Property(x => x.Liked)
                .HasDefaultValue(1);

            builder.Property(x => x.ViewCount)
                .HasDefaultValue(1);

            builder.Property(x => x.Pictures)
               .HasMaxLength(512);
        }
    }
}