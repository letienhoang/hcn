using HCN.Units;
using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.Units
{
    public class UnitDto : IEntityDto<Guid>
    {
        public string Name { get; set; }
        public UnitType UnitType { get; set; }
        public string BriefContent { get; set; }
        public bool Visibility { get; set; }
        public Guid Id { get; set; }
    }
}