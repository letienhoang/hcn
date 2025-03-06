using HCN.Units;
using System;

namespace HCN.Admin.Catalog.Units
{
    public class CreateUpdateUnitDto
    {
        public string Name { get; set; }
        public UnitType UnitType { get; set; }
        public string BriefContent { get; set; }
        public bool Visibility { get; set; }
    }
}