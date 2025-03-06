using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace HCN.Reviews
{
    public class Review : FullAuditedEntity<Guid>
    {
        public Review()
        { }
        public string Title { get; set; }
        public int? Star { get; set; }
        public Guid PostId { get; set; }
        public ReviewType ReviewType { get; set; }
        public Guid? ParentId { get; set; }
        public string Content { get; set; }
        public decimal? Expense { get; set; }
        public int Liked { get; set; }
        public int ViewCount { get; set; }
        public string Pictures { get; set; }
        public int SortOrder { get; set; }
        public bool Visibility { get; set; }
    }
}