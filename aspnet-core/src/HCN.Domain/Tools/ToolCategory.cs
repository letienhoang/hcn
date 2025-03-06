using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace HCN.Tools
{
    public class ToolCategory : CreationAuditedAggregateRoot<Guid>
    {
        public ToolCategory()
        { }
        public ToolCategory(Guid id, string name, string slug, string coverPicture,
            string description, string keywordSEO, string descriptionSEO,
            Guid? parentId, bool visibility)
        {
            Id = id;
            Name = name;
            Slug = slug;
            CoverPicture = coverPicture;
            Description = description;
            KeywordSEO = keywordSEO;
            DescriptionSEO = descriptionSEO;
            ParentId = parentId;
            Visibility = visibility;
        }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string CoverPicture { get; set; }
        public string Description { get; set; }
        public string KeywordSEO { get; set; }
        public string DescriptionSEO { get; set; }
        public Guid? ParentId { get; set; }
        public bool Visibility { get; set; }
    }
}