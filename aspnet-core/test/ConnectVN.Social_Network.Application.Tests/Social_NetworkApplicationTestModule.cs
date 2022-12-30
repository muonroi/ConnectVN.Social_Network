using ConnectVN.Social_Network.Customer;
using Volo.Abp.Modularity;

namespace ConnectVN.Social_Network;

[DependsOn(
    typeof(Social_NetworkCustomerApplicationModule),
    typeof(Social_NetworkDomainTestModule)
    )]
public class Social_NetworkApplicationTestModule : AbpModule
{

}
