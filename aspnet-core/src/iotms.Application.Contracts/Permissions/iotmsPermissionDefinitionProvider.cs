using iotms.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace iotms.Permissions;

public class iotmsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(iotmsPermissions.GroupName);

        myGroup.AddPermission(iotmsPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(iotmsPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(iotmsPermissions.MyPermission1, L("Permission:MyPermission1"));

        var accountPermission = myGroup.AddPermission(iotmsPermissions.Accounts.Default, L("Permission:Accounts"));
        accountPermission.AddChild(iotmsPermissions.Accounts.Create, L("Permission:Create"));
        accountPermission.AddChild(iotmsPermissions.Accounts.Edit, L("Permission:Edit"));
        accountPermission.AddChild(iotmsPermissions.Accounts.Delete, L("Permission:Delete"));

        var devicePermission = myGroup.AddPermission(iotmsPermissions.Devices.Default, L("Permission:Devices"));
        devicePermission.AddChild(iotmsPermissions.Devices.Create, L("Permission:Create"));
        devicePermission.AddChild(iotmsPermissions.Devices.Edit, L("Permission:Edit"));
        devicePermission.AddChild(iotmsPermissions.Devices.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<iotmsResource>(name);
    }
}