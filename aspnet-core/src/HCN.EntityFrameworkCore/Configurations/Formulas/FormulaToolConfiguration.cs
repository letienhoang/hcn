using HCN.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Formulas
{
    public class FormulaToolConfiguration : IEntityTypeConfiguration<FormulaTool>
    {
        public void Configure(EntityTypeBuilder<FormulaTool> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "FormulaTools");

            builder.HasKey(x => new { x.FormulaId, x.ToolId });

            builder.Property(x => x.Value)
                .HasColumnType("decimal(19,4)")
                .IsRequired();
        }
    }
}