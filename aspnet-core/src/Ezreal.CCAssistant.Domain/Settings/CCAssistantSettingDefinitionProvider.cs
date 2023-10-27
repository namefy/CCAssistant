using Volo.Abp.Settings;

namespace Ezreal.CCAssistant.Settings;

public class CCAssistantSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CCAssistantSettings.MySetting1));
    }
}
