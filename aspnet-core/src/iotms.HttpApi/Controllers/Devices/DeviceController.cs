using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using iotms.Devices;

namespace iotms.Controllers.Devices
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Device")]
    [Route("api/app/devices")]

    public abstract class DeviceControllerBase : AbpController
    {
        protected IDevicesAppService _devicesAppService;

        public DeviceControllerBase(IDevicesAppService devicesAppService)
        {
            _devicesAppService = devicesAppService;
        }

        [HttpGet]
        [Route("by-account")]
        public virtual Task<PagedResultDto<DeviceDto>> GetListByAccountIdAsync(GetDeviceListInput input)
        {
            return _devicesAppService.GetListByAccountIdAsync(input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<DeviceDto>> GetListAsync(GetDevicesInput input)
        {
            return _devicesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<DeviceDto> GetAsync(Guid id)
        {
            return _devicesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<DeviceDto> CreateAsync(DeviceCreateDto input)
        {
            var device = _devicesAppService.CreateAsync(input);
            return device;
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<DeviceDto> UpdateAsync(Guid id, DeviceUpdateDto input)
        {
            return _devicesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _devicesAppService.DeleteAsync(id);
        }
    }
}