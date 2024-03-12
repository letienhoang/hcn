namespace HCN.Admin.Permissions;

public static class AdminPermissions
{
    public const string SystemGroupName = "HCNAdminSystem";
    public const string CatalogGroupName = "HCNAdminCatalog";

    //Add your own permission names. Example:
    public static class Formula
    {
        public const string Default = CatalogGroupName + ".Formula";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class FormulaCategory
    {
        public const string Default = CatalogGroupName + ".FormulaCategory";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class MaterialCategory
    {
        public const string Default = CatalogGroupName + ".MaterialCategory";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class ToolCategory
    {
        public const string Default = CatalogGroupName + ".ToolCategory";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
    public static class Topic
    {
        public const string Default = CatalogGroupName + ".Topic";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
}
