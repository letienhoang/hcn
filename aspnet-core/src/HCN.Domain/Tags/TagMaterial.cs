using System;
using Volo.Abp.Domain.Entities;

namespace HCN.Tags
{
    public class TagMaterial : Entity
    {
        public TagMaterial()
        { }

        public Guid MaterialId { get; set; }
        public Guid TagId { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { MaterialId, TagId };
        }
    }
}