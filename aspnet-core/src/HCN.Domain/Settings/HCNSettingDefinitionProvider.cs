using Volo.Abp.Settings;

namespace HCN.Settings;

public class HCNSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(HCNSettings.MySetting1));
    }
}
