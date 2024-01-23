using HCN.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Tags
{
    public class TagToolConfiguration : IEntityTypeConfiguration<TagTool>
    {
        public void Configure(EntityTypeBuilder<TagTool> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "TagTools");

            builder.HasKey(x => new { x.TagId, x.ToolId });
        }
    }
}