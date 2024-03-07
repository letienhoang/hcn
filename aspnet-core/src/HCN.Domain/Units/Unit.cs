using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace HCN.Units
{
    public class Unit : CreationAuditedAggregateRoot<Guid>
    {
        public Unit()
        { }
        public string Name { get; set; }
        public UnitType UnitType { get; set; }
        public string BriefContent { get; set; }
        public bool Visibility { get; set; }
    }
}