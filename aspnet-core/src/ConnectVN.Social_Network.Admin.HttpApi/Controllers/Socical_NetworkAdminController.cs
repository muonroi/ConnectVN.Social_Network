using ConnectVN.Social_Network.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ConnectVN.Social_Network.Admin.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class Social_NetworkAdminController : AbpControllerBase
{
    protected Social_NetworkAdminController()
    {
        LocalizationResource = typeof(Social_NetworkResource);
    }
}
