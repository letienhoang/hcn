using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.Stories
{
    public class StoryInListDto : EntityDto<Guid>
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public string ThumbnailPicture { get; set; }
        public int Liked { get; set; }
        public int ViewCount { get; set; }
        public int SortOrder { get; set; }
        public bool Visibility { get; set; }
        public string TopicName { get; set; }
        public string TopicSlug { get; set; }
    }
}