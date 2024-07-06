using Volo.Abp.Modularity;

namespace iotms;

public abstract class iotmsApplicationTestBase<TStartupModule> : iotmsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
