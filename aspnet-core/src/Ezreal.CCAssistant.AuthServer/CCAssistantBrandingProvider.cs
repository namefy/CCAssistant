using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Ezreal.CCAssistant;

[Dependency(ReplaceServices = true)]
public class CCAssistantBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CCAssistant";
}
