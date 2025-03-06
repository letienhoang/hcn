using HCN.Units;
using System;
using Volo.Abp.Application.Dtos;

namespace HCN.Admin.Catalog.Units
{
    public class UnitInListDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public UnitType UnitType { get; set; }
        public string BriefContent { get; set; }
        public bool Visibility { get; set; }
    }
}