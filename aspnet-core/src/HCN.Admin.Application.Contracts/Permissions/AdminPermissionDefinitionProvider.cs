using HCN.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HCN.Admin.Permissions;

public class AdminPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //Catalog
        var catalogGroup = context.AddGroup(AdminPermissions.CatalogGroupName, L("Permission:HCNAdminCatalog"));

        //Formula
        var formulaPermission = catalogGroup.AddPermission(AdminPermissions.Formula.Default, L("Permission:HCNAdminCatalog.Formula"));
        formulaPermission.AddChild(AdminPermissions.Formula.Create, L("Permission:HCNAdminCatalog.Formula.Create"));
        formulaPermission.AddChild(AdminPermissions.Formula.Update, L("Permission:HCNAdminCatalog.Formula.Update"));
        formulaPermission.AddChild(AdminPermissions.Formula.Delete, L("Permission:HCNAdminCatalog.Formula.Delete"));

        //Formula category
        var formulaCategoryPermission = catalogGroup.AddPermission(AdminPermissions.FormulaCategory.Default, L("Permission:HCNAdminCatalog.FormulaCategory"));
        formulaCategoryPermission.AddChild(AdminPermissions.FormulaCategory.Create, L("Permission:HCNAdminCatalog.FormulaCategory.Create"));
        formulaCategoryPermission.AddChild(AdminPermissions.FormulaCategory.Update, L("Permission:HCNAdminCatalog.FormulaCategory.Update"));
        formulaCategoryPermission.AddChild(AdminPermissions.FormulaCategory.Delete, L("Permission:HCNAdminCatalog.FormulaCategory.Delete"));

        //Material category
        var materialCategoryPermission = catalogGroup.AddPermission(AdminPermissions.MaterialCategory.Default, L("Permission:HCNAdminCatalog.MaterialCategory"));
        materialCategoryPermission.AddChild(AdminPermissions.MaterialCategory.Create, L("Permission:HCNAdminCatalog.MaterialCategory.Create"));
        materialCategoryPermission.AddChild(AdminPermissions.MaterialCategory.Update, L("Permission:HCNAdminCatalog.MaterialCategory.Update"));
        materialCategoryPermission.AddChild(AdminPermissions.MaterialCategory.Delete, L("Permission:HCNAdminCatalog.MaterialCategory.Delete"));

        //Tool category
        var toolCategoryPermission = catalogGroup.AddPermission(AdminPermissions.ToolCategory.Default, L("Permission:HCNAdminCatalog.ToolCategory"));
        toolCategoryPermission.AddChild(AdminPermissions.ToolCategory.Create, L("Permission:HCNAdminCatalog.ToolCategory.Create"));
        toolCategoryPermission.AddChild(AdminPermissions.ToolCategory.Update, L("Permission:HCNAdminCatalog.ToolCategory.Update"));
        toolCategoryPermission.AddChild(AdminPermissions.ToolCategory.Delete, L("Permission:HCNAdminCatalog.ToolCategory.Delete"));

        //Topic
        var topicPermission = catalogGroup.AddPermission(AdminPermissions.Topic.Default, L("Permission:HCNAdminCatalog.Topic"));
        topicPermission.AddChild(AdminPermissions.Topic.Create, L("Permission:HCNAdminCatalog.Topic.Create"));
        topicPermission.AddChild(AdminPermissions.Topic.Update, L("Permission:HCNAdminCatalog.Topic.Update"));
        topicPermission.AddChild(AdminPermissions.Topic.Delete, L("Permission:HCNAdminCatalog.Topic.Delete"));

        //Story
        var storyPermission = catalogGroup.AddPermission(AdminPermissions.Story.Default, L("Permission:HCNAdminCatalog.Story"));
        storyPermission.AddChild(AdminPermissions.Story.Create, L("Permission:HCNAdminCatalog.Story.Create"));
        storyPermission.AddChild(AdminPermissions.Story.Update, L("Permission:HCNAdminCatalog.Story.Update"));
        storyPermission.AddChild(AdminPermissions.Story.Delete, L("Permission:HCNAdminCatalog.Story.Delete"));

        //Tag
        var tagPermission = catalogGroup.AddPermission(AdminPermissions.Tag.Default, L("Permission:HCNAdminCatalog.Tag"));
        tagPermission.AddChild(AdminPermissions.Tag.Create, L("Permission:HCNAdminCatalog.Tag.Create"));
        tagPermission.AddChild(AdminPermissions.Tag.Update, L("Permission:HCNAdminCatalog.Tag.Update"));
        tagPermission.AddChild(AdminPermissions.Tag.Delete, L("Permission:HCNAdminCatalog.Tag.Delete"));

        //Material
        var materialPermission = catalogGroup.AddPermission(AdminPermissions.Material.Default, L("Permission:HCNAdminCatalog.Material"));
        materialPermission.AddChild(AdminPermissions.Material.Create, L("Permission:HCNAdminCatalog.Material.Create"));
        materialPermission.AddChild(AdminPermissions.Material.Update, L("Permission:HCNAdminCatalog.Material.Update"));
        materialPermission.AddChild(AdminPermissions.Material.Delete, L("Permission:HCNAdminCatalog.Material.Delete"));

        //Tool
        var toolPermission = catalogGroup.AddPermission(AdminPermissions.Tool.Default, L("Permission:HCNAdminCatalog.Tool"));
        toolPermission.AddChild(AdminPermissions.Tool.Create, L("Permission:HCNAdminCatalog.Tool.Create"));
        toolPermission.AddChild(AdminPermissions.Tool.Update, L("Permission:HCNAdminCatalog.Tool.Update"));
        toolPermission.AddChild(AdminPermissions.Tool.Delete, L("Permission:HCNAdminCatalog.Tool.Delete"));

        //Unit
        var unitPermission = catalogGroup.AddPermission(AdminPermissions.Unit.Default, L("Permission:HCNAdminCatalog.Unit"));
        unitPermission.AddChild(AdminPermissions.Unit.Create, L("Permission:HCNAdminCatalog.Unit.Create"));
        unitPermission.AddChild(AdminPermissions.Unit.Update, L("Permission:HCNAdminCatalog.Unit.Update"));
        unitPermission.AddChild(AdminPermissions.Unit.Delete, L("Permission:HCNAdminCatalog.Unit.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HCNResource>(name);
    }
}