using HCN.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Formulas
{
    public class FormulaCategoryConfiguration : IEntityTypeConfiguration<FormulaCategory>
    {
        public void Configure(EntityTypeBuilder<FormulaCategory> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "FormulaCategories");

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