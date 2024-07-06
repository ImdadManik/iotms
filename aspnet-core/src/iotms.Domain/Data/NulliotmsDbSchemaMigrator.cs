using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace iotms.Data;

/* This is used if database provider does't define
 * IiotmsDbSchemaMigrator implementation.
 */
public class NulliotmsDbSchemaMigrator : IiotmsDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
