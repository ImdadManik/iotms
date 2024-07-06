using Volo.Abp.Settings;

namespace iotms.Settings;

public class iotmsSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(iotmsSettings.MySetting1));
    }
}
