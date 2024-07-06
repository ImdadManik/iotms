using Volo.Abp.Modularity;

namespace iotms;

[DependsOn(
    typeof(iotmsDomainModule),
    typeof(iotmsTestBaseModule)
)]
public class iotmsDomainTestModule : AbpModule
{

}
