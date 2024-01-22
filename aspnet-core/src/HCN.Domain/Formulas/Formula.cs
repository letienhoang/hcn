using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace HCN.Formulas
{
    public class Formula : AuditedAggregateRoot<Guid>
    {
        public Formula()
        { }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Level Level { get; set; }
        public int? ExecutionTime { get; set; }
        public string ThumbnailPicture { get; set; }
        public string Description { get; set; }
        public int? SortOrder { get; set; }
        public bool Visibility { get; set; }
        public string VideoUrl { get; set; }
        public string ReferenceSource { get; set; }
        public string KeywordSEO { get; set; }
        public string DescriptionSEO { get; set; }
    }
}