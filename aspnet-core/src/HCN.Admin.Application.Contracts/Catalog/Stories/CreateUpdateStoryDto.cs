using System;

namespace HCN.Admin.Catalog.Stories
{
    public class CreateUpdateStoryDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public string BriefContent { get; set; }
        public string Content { get; set; }
        public string Pictures { get; set; }
        public int Liked { get; set; }
        public int ViewCount { get; set; }
        public int SortOrder { get; set; }
        public bool Visibility { get; set; }
        public string ReferenceSource { get; set; }
        public string KeywordSEO { get; set; }
        public string DescriptionSEO { get; set; }
        public Guid? TopicId { get; set; }
        public string CoverPictureName { get; set; }
        public string CoverPictureContent { get; set; }
        public string[] Tags { get; set; }
    }
}