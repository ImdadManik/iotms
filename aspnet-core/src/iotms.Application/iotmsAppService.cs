using iotms.Localization;
using Volo.Abp.Application.Services;

namespace iotms;

/* Inherit your application services from this class.
 */
public abstract class iotmsAppService : ApplicationService
{
    protected iotmsAppService()
    {
        LocalizationResource = typeof(iotmsResource);
    }
}
