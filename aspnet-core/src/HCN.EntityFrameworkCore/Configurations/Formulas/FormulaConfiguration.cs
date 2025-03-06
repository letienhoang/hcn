using HCN.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Formulas
{
    public class FormulaConfiguration : IEntityTypeConfiguration<Formula>
    {
        public void Configure(EntityTypeBuilder<Formula> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "Formulas");

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

            builder.Property(x => x.ExecutionTime)
                .HasDefaultValue(0);

            builder.Property(x => x.ThumbnailPicture)
               .HasMaxLength(512);

            builder.Property(x => x.BriefContent)
               .HasMaxLength(1024);

            builder.Property(x => x.VideoUrl)
               .HasMaxLength(512);

            builder.Property(x => x.ReferenceSource)
               .HasMaxLength(512);

            builder.Property(x => x.KeywordSEO)
               .HasMaxLength(512);

            builder.Property(x => x.DescriptionSEO)
               .HasMaxLength(1024);
        }
    }
}