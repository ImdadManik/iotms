using Volo.Abp.Modularity;

namespace iotms;

/* Inherit from this class for your domain layer tests. */
public abstract class iotmsDomainTestBase<TStartupModule> : iotmsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
