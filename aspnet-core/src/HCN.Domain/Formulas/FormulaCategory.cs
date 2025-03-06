using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace HCN.Formulas
{
    public class FormulaCategory : CreationAuditedAggregateRoot<Guid>
    {
        public FormulaCategory()
        { }

        public FormulaCategory(Guid id, string name, string slug, 
            string coverPicture, string description, bool visibility, 
            string keywordSEO, string descriptionSEO, Guid? parentId)
        {
            Id = id;
            Name = name;
            Slug = slug;
            CoverPicture = coverPicture;
            Description = description;
            Visibility = visibility;
            KeywordSEO = keywordSEO;
            DescriptionSEO = descriptionSEO;
            ParentId = parentId;
        }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string CoverPicture { get; set; }
        public string Description { get; set; }
        public bool Visibility { get; set; }
        public string KeywordSEO { get; set; }
        public string DescriptionSEO { get; set; }
        public Guid? ParentId { get; set; }
    }
}