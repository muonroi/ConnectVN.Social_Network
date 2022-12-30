using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ConnectVN.Social_Network.Authen;

[Dependency(ReplaceServices = true)]
public class Social_NetworkBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Social_Network";
}
