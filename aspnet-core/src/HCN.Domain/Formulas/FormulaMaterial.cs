using System;
using Volo.Abp.Domain.Entities;

namespace HCN.Formulas
{
    public class FormulaMaterial : Entity
    {
        public FormulaMaterial()
        { }

        public Guid FormulaId { get; set; }
        public Guid MaterialId { get; set; }
        public decimal Value { get; set; }
        public Guid UnitId { get; set; }
        public string Description { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { FormulaId, MaterialId };
        }
    }
}