using System;
using Volo.Abp.Domain.Entities;

namespace HCN.Tools
{
    public class ToolFormula : Entity
    {
        public ToolFormula()
        { }

        public Guid FormulaId { get; set; }
        public Guid ToolId { get; set; }
        public decimal Value { get; set; }
        public Guid UnitId { get; set; }
        public string Description { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { FormulaId, ToolId };
        }
    }
}