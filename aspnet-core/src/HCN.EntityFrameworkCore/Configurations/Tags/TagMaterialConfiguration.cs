using HCN.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Tags
{
    public class TagMaterialConfiguration : IEntityTypeConfiguration<TagMaterial>
    {
        public void Configure(EntityTypeBuilder<TagMaterial> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "TagMaterials");

            builder.HasKey(x => new { x.TagId, x.MaterialId });
        }
    }
}