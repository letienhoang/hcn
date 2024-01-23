using HCN.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Tags
{
    public class TagStepConfiguration : IEntityTypeConfiguration<TagStep>
    {
        public void Configure(EntityTypeBuilder<TagStep> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "TagSteps");

            builder.HasKey(x => new { x.TagId, x.StepId });
        }
    }
}