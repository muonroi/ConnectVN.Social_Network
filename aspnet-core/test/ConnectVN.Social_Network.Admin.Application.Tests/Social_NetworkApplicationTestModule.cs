using ConnectVN.Social_Network.Admin;
using Volo.Abp.Modularity;

namespace ConnectVN.Social_Network;

[DependsOn(
    typeof(Social_NetworkAdminApplicationModule),
    typeof(Social_NetworkDomainTestModule)
    )]
public class Social_NetworkApplicationTestModule : AbpModule
{

}
