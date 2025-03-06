using HCN.Stories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Stories
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "Topics");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
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

            builder.Property(x => x.CoverPicture)
               .HasMaxLength(512);

            builder.Property(x => x.KeywordSEO)
               .HasMaxLength(512);

            builder.Property(x => x.DescriptionSEO)
               .HasMaxLength(1024);
        }
    }
}