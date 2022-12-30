using System;
using System.Collections.Generic;
using System.Text;
using ConnectVN.Social_Network.Localization;
using Volo.Abp.Application.Services;

namespace ConnectVN.Social_Network.Customer;

/* Inherit your application services from this class.
 */
public abstract class Social_NetworkCustomerAppService : ApplicationService
{
    protected Social_NetworkCustomerAppService()
    {
        LocalizationResource = typeof(Social_NetworkResource);
    }
}
