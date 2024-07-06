using Volo.Abp.Modularity;

namespace iotms;

[DependsOn(
    typeof(iotmsApplicationModule),
    typeof(iotmsDomainTestModule)
)]
public class iotmsApplicationTestModule : AbpModule
{

}
