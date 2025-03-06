using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace HCN.Tools
{
    public class Tool : AuditedAggregateRoot<Guid>
    {
        public Tool()
        { }

        public Tool(Guid id, string name, string slug, string code,
            Guid categoryId, ToolType toolType, string description,
            string thumnailPicture, string pictures, bool visibility,
            string keywordSEO, string descriptionSEO, Guid? parentId)
        {
            Id = id;
            Name = name;
            Slug = slug;
            Code = code;
            CategoryId = categoryId;
            ToolType = toolType;
            Description = description;
            ThumbnailPicture = thumnailPicture;
            Pictures = pictures;
            Visibility = visibility;
            KeywordSEO = keywordSEO;
            DescriptionSEO = descriptionSEO;
            ParentId = parentId;
        }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public Guid CategoryId { get; set; }
        public ToolType ToolType { get; set; }
        public string Description { get; set; }
        public string ThumbnailPicture { get; set; }
        public string Pictures { get; set; }
        public string KeywordSEO { get; set; }
        public string DescriptionSEO { get; set; }
        public Guid? ParentId { get; set; }
        public bool Visibility { get; set; }
    }
}