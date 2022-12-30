using ConnectVN.Social_Network.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ConnectVN.Social_Network.Permissions;

public class Social_NetworkCustomerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(Social_NetworkCustomerPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(Social_NetworkPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Social_NetworkResource>(name);
    }
}
