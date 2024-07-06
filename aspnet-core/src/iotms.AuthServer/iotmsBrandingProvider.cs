using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace iotms;

[Dependency(ReplaceServices = true)]
public class iotmsBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "iotms";
}
