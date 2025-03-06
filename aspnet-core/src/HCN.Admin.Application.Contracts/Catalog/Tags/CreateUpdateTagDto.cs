using System;

namespace HCN.Admin.Catalog.Tags
{
    public class CreateUpdateTagDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool Visibility { get; set; }
    }
}