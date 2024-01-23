using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace HCN.Stories
{
    public class Story : AuditedAggregateRoot<Guid>
    {
        public Story()
        { }
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
        public Guid? ParentId { get; set; }
    }
}