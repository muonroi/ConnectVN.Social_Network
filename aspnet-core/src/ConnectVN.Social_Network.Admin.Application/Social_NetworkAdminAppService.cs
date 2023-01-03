using ConnectVN.Social_Network.Localization;
using Volo.Abp.Application.Services;

namespace ConnectVN.Social_Network.Admin;

/* Inherit your application services from this class.
 */
public abstract class Social_NetworkAdminAppService : ApplicationService
{
    protected Social_NetworkAdminAppService()
    {
        LocalizationResource = typeof(Social_NetworkResource);
    }
}
