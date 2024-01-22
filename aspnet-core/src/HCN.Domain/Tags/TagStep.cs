using System;
using Volo.Abp.Domain.Entities;

namespace HCN.Tags
{
    public class TagStep : Entity
    {
        public TagStep()
        { }

        public Guid StepId { get; set; }
        public Guid TagId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { StepId, TagId };
        }
    }
}