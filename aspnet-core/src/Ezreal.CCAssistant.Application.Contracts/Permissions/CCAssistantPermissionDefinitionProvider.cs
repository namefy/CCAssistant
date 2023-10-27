using Ezreal.CCAssistant.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ezreal.CCAssistant.Permissions;

public class CCAssistantPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CCAssistantPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(CCAssistantPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CCAssistantResource>(name);
    }
}
