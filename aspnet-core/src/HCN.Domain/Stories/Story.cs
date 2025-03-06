using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace HCN.Stories
{
    public class Story : AuditedAggregateRoot<Guid>
    {
        public Story()
        { }
        public Story(Guid id, string title, string slug, string code, string briefContent, string content, 
            string thumnailPicture, string pictures, int liked, int viewCount, int sortOrder, bool visibility,
            string referenceSource, string keywordSEO, string descriptionSEO, Guid? topicId)
        {
            Id = id;
            Title = title;
            Slug = slug;
            Code = code;
            BriefContent = briefContent;
            Content = content;
            ThumbnailPicture = thumnailPicture;
            Pictures = pictures;
            Liked = liked;
            ViewCount = viewCount;
            SortOrder = sortOrder;
            Visibility = visibility;
            ReferenceSource = referenceSource;
            KeywordSEO = keywordSEO;
            DescriptionSEO = descriptionSEO;
            TopicId = topicId;
        }

        public string Title { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public string BriefContent { get; set; }
        public string Content { get; set; }
        public string ThumbnailPicture { get; set; }
        public string Pictures { get; set; }
        public int Liked { get; set; }
        public int ViewCount { get; set; }
        public int SortOrder { get; set; }
        public bool Visibility { get; set; }
        public string ReferenceSource { get; set; }
        public string KeywordSEO { get; set; }
        public string DescriptionSEO { get; set; }
        public Guid? TopicId { get; set; }
    }
}