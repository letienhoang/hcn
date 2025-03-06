using System;

namespace HCN.Admin.Catalog.Stories
{
    public class StoryListFilterDto : BaseListFilterDto
    {
        public Guid? TopicId { get; set; }
    }
}