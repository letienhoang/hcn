using System;
using Volo.Abp.Domain.Entities;

namespace HCN.Tags
{
    public class TagFormula : Entity
    {
        public TagFormula()
        { }

        public Guid FormulaId { get; set; }
        public Guid TagId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { FormulaId, TagId };
        }
    }
}