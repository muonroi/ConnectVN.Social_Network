using ConnectVN.Social_Network.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ConnectVN.Social_Network.Admin.Permissions;

public class Social_NetworkAdminPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup("Administrator");
        myGroup.AddPermission("Administrator_Author_Create");

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Social_NetworkResource>(name);
    }
}
