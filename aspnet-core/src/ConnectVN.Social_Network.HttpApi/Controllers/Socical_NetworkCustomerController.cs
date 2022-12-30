using ConnectVN.Social_Network.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ConnectVN.Social_Network.Customer.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class Social_NetworkCustomerController : AbpControllerBase
{
    protected Social_NetworkCustomerController()
    {
        LocalizationResource = typeof(Social_NetworkResource);
    }
}
