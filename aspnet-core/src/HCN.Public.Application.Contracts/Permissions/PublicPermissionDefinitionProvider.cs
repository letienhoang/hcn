using HCN.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HCN.Public.Permissions;

public class PublicPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PublicPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(PublicPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HCNResource>(name);
    }
}