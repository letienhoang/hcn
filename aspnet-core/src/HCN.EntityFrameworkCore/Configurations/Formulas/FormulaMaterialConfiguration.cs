using HCN.Formulas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Formulas
{
    public class FormulaMaterialConfiguration : IEntityTypeConfiguration<FormulaMaterial>
    {
        public void Configure(EntityTypeBuilder<FormulaMaterial> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "FormulaMaterials");

            builder.HasKey(x => new { x.FormulaId, x.MaterialId });

            builder.Property(x => x.Value)
                .HasColumnType("decimal(19,4)")
                .IsRequired();
        }
    }
}