using HCN.Materials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Materials
{
    public class MaterialCategoryConfiguration : IEntityTypeConfiguration<MaterialCategory>
    {
        public void Configure(EntityTypeBuilder<MaterialCategory> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "MaterialCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Slug)
                .HasMaxLength(256)
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