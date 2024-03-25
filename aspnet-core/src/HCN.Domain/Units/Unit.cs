using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace HCN.Units
{
    public class Unit : CreationAuditedAggregateRoot<Guid>
    {
        public Unit()
        { }

        public Unit(Guid id, string name, UnitType unitType, string briefContent, bool visibility) {
            Id = id;
            Name = name;
            UnitType = unitType;
            BriefContent = briefContent;
            Visibility = visibility;
        }

        public string Name { get; set; }
        public UnitType UnitType { get; set; }
        public string BriefContent { get; set; }
        public bool Visibility { get; set; }
    }
}