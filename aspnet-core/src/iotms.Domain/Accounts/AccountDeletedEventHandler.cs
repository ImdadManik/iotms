using iotms.Devices;

using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace iotms.Accounts;

public class AccountDeletedEventHandler : ILocalEventHandler<EntityDeletedEventData<Account>>, ITransientDependency
{
    private readonly IDeviceRepository _deviceRepository;

    public AccountDeletedEventHandler(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;

    }

    public async Task HandleEventAsync(EntityDeletedEventData<Account> eventData)
    {
        if (eventData.Entity is not ISoftDelete softDeletedEntity)
        {
            return;
        }

        if (!softDeletedEntity.IsDeleted)
        {
            return;
        }

        try
        {
            await _deviceRepository.DeleteManyAsync(await _deviceRepository.GetListByAccountIdAsync(eventData.Entity.Id));

        }
        catch
        {
            //...
        }
    }
}