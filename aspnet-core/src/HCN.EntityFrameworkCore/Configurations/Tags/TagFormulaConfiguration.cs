using HCN.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Tags
{
    public class TagFormulaConfiguration : IEntityTypeConfiguration<TagFormula>
    {
        public void Configure(EntityTypeBuilder<TagFormula> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "TagFormulas");

            builder.HasKey(x => new { x.TagId, x.FormulaId });
        }
    }
}