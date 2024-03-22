using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace HCN.Formulas
{
    public class Formula : AuditedAggregateRoot<Guid>
    {
        public Formula()
        { }

        public Formula(Guid id, string name, string slug, string code, Guid categoryId,
            Level level, FormulaType formulaType, int executionTime, string thumbnailPicture,
            string briefContent, string description, int liked, int viewCount, int sortOrder,
            bool visibility, string videoUrl, string referenceSource, string keywordSEO, string descriptionSEO, Guid? parentId)
        {
            Id = id; Name = name; Slug = slug; Code = code; CategoryId = categoryId; Level = level;
            FormulaType = formulaType; ExecutionTime = executionTime; ThumbnailPicture = thumbnailPicture;
            BriefContent = briefContent; Description = description; Liked = liked; ViewCount = viewCount;
            SortOrder = sortOrder; Visibility = visibility; VideoUrl = videoUrl; ReferenceSource = referenceSource;
            KeywordSEO = keywordSEO; DescriptionSEO = descriptionSEO; ParentId = parentId;
        }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public Guid CategoryId { get; set; }
        public Level Level { get; set; }
        public FormulaType FormulaType { get; set; }
        public int ExecutionTime { get; set; }
        public string ThumbnailPicture { get; set; }
        public string BriefContent { get; set; }
        public string Description { get; set; }
        public int Liked { get; set; }
        public int ViewCount { get; set; }
        public int SortOrder { get; set; }
        public bool Visibility { get; set; }
        public string VideoUrl { get; set; }
        public string ReferenceSource { get; set; }
        public string KeywordSEO { get; set; }
        public string DescriptionSEO { get; set; }
        public Guid? ParentId { get; set; }
    }
}