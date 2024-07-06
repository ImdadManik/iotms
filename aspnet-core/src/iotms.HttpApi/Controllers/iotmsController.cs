using iotms.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace iotms.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class iotmsController : AbpControllerBase
{
    protected iotmsController()
    {
        LocalizationResource = typeof(iotmsResource);
    }
}
