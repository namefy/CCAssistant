using Ezreal.CCAssistant.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ezreal.CCAssistant.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CCAssistantController : AbpControllerBase
{
    protected CCAssistantController()
    {
        LocalizationResource = typeof(CCAssistantResource);
    }
}
