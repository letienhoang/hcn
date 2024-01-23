using HCN.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Materials
{
    public class ToolConfiguration : IEntityTypeConfiguration<Tool>
    {
        public void Configure(EntityTypeBuilder<Tool> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "Tools");

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

            builder.Property(x => x.Pictures)
               .HasMaxLength(512);

            builder.Property(x => x.KeywordSEO)
               .HasMaxLength(512);

            builder.Property(x => x.DescriptionSEO)
               .HasMaxLength(1024);
        }
    }
}