using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace iotms.Devices
{
    public partial interface IDevicesAppService : IApplicationService
    {

        Task<PagedResultDto<DeviceDto>> GetListByAccountIdAsync(GetDeviceListInput input);

        Task<PagedResultDto<DeviceDto>> GetListAsync(GetDevicesInput input);

        Task<DeviceDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<DeviceDto> CreateAsync(DeviceCreateDto input);

        Task<DeviceDto> UpdateAsync(Guid id, DeviceUpdateDto input);
    }
}