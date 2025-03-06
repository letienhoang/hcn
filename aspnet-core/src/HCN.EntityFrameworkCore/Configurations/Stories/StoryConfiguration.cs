using HCN.Stories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Stories
{
    public class StoryConfiguration : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "Stories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Slug)
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.Code)
                 .HasMaxLength(128)
                 .IsUnicode(false)
                 .IsRequired();

            builder.Property(x => x.BriefContent)
               .HasMaxLength(1024);
            
            builder.Property(x => x.ThumbnailPicture)
               .HasMaxLength(512);

            builder.Property(x => x.Pictures)
               .HasMaxLength(512);

            builder.Property(x => x.Liked)
                .HasDefaultValue(1);

            builder.Property(x => x.ViewCount)
                .HasDefaultValue(1);

            builder.Property(x => x.ReferenceSource)
               .HasMaxLength(512);

            builder.Property(x => x.KeywordSEO)
               .HasMaxLength(512);

            builder.Property(x => x.DescriptionSEO)
               .HasMaxLength(1024);
        }
    }
}