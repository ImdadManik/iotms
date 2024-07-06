using System.Threading.Tasks;

namespace iotms.Data;

public interface IiotmsDbSchemaMigrator
{
    Task MigrateAsync();
}
