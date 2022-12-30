using Volo.Abp.Settings;

namespace ConnectVN.Social_Network.Common.Settings;

public class Social_NetworkSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(Social_NetworkSettings.MySetting1));
    }
}
