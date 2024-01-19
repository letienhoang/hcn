using HCN.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HCN.Permissions;

public class HCNPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HCNPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(HCNPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HCNResource>(name);
    }
}
