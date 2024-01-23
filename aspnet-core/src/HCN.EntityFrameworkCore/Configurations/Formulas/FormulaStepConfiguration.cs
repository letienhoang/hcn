using HCN.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Formulas
{
    public class FormulaStepConfiguration : IEntityTypeConfiguration<FormulaStep>
    {
        public void Configure(EntityTypeBuilder<FormulaStep> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "FormulaSteps");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Pictures)
               .HasMaxLength(512);

            builder.Property(x => x.Content)
               .IsRequired();
        }
    }
}