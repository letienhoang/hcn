using System;
using Volo.Abp.Domain.Entities;

namespace HCN.Tags
{
    public class Tag : Entity<Guid>
    {
        public Tag()
        { }
        public Tag(Guid id, string name, string slug, bool visibility)
        { 
            Id = id;
            Name = name;
            Slug = slug;
            Visibility = visibility;
        }

        public string Name { get; set; }
        public string Slug { get; set; }
        public bool Visibility { get; set; }
    }
}