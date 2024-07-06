using Asp.Versioning;
using System;
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

    public class DeviceController : DeviceControllerBase, IDevicesAppService
    {
        public DeviceController(IDevicesAppService devicesAppService) : base(devicesAppService)
        {
        }
    }
}