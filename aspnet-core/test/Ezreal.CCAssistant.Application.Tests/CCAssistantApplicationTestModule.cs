using Volo.Abp.Modularity;

namespace Ezreal.CCAssistant;

[DependsOn(
    typeof(CCAssistantApplicationModule),
    typeof(CCAssistantDomainTestModule)
    )]
public class CCAssistantApplicationTestModule : AbpModule
{

}
