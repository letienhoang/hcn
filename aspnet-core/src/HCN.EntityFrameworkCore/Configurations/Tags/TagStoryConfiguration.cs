using HCN.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCN.Configurations.Tags
{
    public class TagStoryConfiguration : IEntityTypeConfiguration<TagStory>
    {
        public void Configure(EntityTypeBuilder<TagStory> builder)
        {
            builder.ToTable(HCNConsts.DbTablePrefix + "TagStories");

            builder.HasKey(x => new { x.TagId, x.StoryId });
        }
    }
}