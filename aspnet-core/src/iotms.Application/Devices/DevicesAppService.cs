using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using iotms.Permissions;
using iotms.Emqx_UserAuth; 
using iotms.Emqx;
using Newtonsoft.Json; 

namespace iotms.Devices
{
    [RemoteService(IsEnabled = false)]
    [Authorize(iotmsPermissions.Devices.Default)]
    public abstract class DevicesAppServiceBase : ApplicationService
    {

        protected IDeviceRepository _deviceRepository;
        protected DeviceManager _deviceManager;

        public DevicesAppServiceBase(IDeviceRepository deviceRepository, DeviceManager deviceManager)
        {

            _deviceRepository = deviceRepository;
            _deviceManager = deviceManager;
        }

        public virtual async Task<PagedResultDto<DeviceDto>> GetListByAccountIdAsync(GetDeviceListInput input)
        {
            var devices = await _deviceRepository.GetListByAccountIdAsync(
                input.AccountId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<DeviceDto>
            {
                TotalCount = await _deviceRepository.GetCountByAccountIdAsync(input.AccountId),
                Items = ObjectMapper.Map<List<Device>, List<DeviceDto>>(devices)
            };
        }

        public virtual async Task<PagedResultDto<DeviceDto>> GetListAsync(GetDevicesInput input)
        {
            var totalCount = await _deviceRepository.GetCountAsync(input.FilterText, input.Name, input.Status, input.Temp, input.LDR, input.PIR, input.Door, input.MinTempAlertMin, input.MinTempAlertMax, input.TempAlertFreqMin, input.TempAlertFreqMax, input.MinLDRAlertMin, input.MinLDRAlertMax, input.LDRAlertFreqMin, input.LDRAlertFreqMax, input.Connection);
            var items = await _deviceRepository.GetListAsync(input.FilterText, input.Name, input.Status, input.Temp, input.LDR, input.PIR, input.Door, input.MinTempAlertMin, input.MinTempAlertMax, input.TempAlertFreqMin, input.TempAlertFreqMax, input.MinLDRAlertMin, input.MinLDRAlertMax, input.LDRAlertFreqMin, input.LDRAlertFreqMax, input.Connection, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DeviceDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Device>, List<DeviceDto>>(items)
            };
        }

        public virtual async Task<DeviceDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Device, DeviceDto>(await _deviceRepository.GetAsync(id));
        }

        [Authorize(iotmsPermissions.Devices.Delete)]
        public virtual async Task DeleteAsync(Guid input)
        {

            Device _device = await _deviceRepository.GetAsync(input);
            cEmqxAPI emqxAPI = new cEmqxAPI();
            var response = emqxAPI.DeleteUsers(_device);

            if (response.IsSuccessful || response.StatusDescription == "Not Found")
            {
                await _deviceRepository.DeleteAsync(input);
            }
        }

        [Authorize(iotmsPermissions.Devices.Create)]
        public virtual async Task<DeviceDto> CreateAsync(DeviceCreateDto input)
        {
            GenerateAES aes = new GenerateAES("098pub+1key+0pri", 256, "ABCXYZ123098");
            cEmqxAPI emqxAPI = new cEmqxAPI();
            var resp = emqxAPI.AddAuthUsers(input.Name, aes.Encrypt(input.Name), false);
            cLogs.Log("UserName: " + input.Name);
            cLogs.Log("pwd:" + aes.Encrypt(input.Name));
            cLogs.Log(resp.Content);

            if (resp.StatusDescription == "Created")
            {
                cLogs.Log(resp.StatusDescription);
                cLogs.Log(input.Name);
                cMsgPublisher.PublishMessage(JsonConvert.SerializeObject(input), input.Name, "device/" + input.Name);
                var device = await _deviceManager.CreateAsync(input.AccountId,
                input.Name, input.Status, input.Temp, input.LDR, input.PIR, input.Door, input.MinTempAlert, input.TempAlertFreq, input.MinLDRAlert, input.LDRAlertFreq, input.Connection
                );
                return ObjectMapper.Map<Device, DeviceDto>(device);
            }
            else
                return null;
        }

        [Authorize(iotmsPermissions.Devices.Edit)]
        public virtual async Task<DeviceDto> UpdateAsync(Guid id, DeviceUpdateDto input)
        {
            cMsgPublisher.PublishMessage(JsonConvert.SerializeObject(input), input.Name, "device/" + input.Name);
            var device = await _deviceManager.UpdateAsync(id, input.AccountId,
            input.Name, input.Status, input.Temp, input.LDR, input.PIR, input.Door, input.MinTempAlert, input.TempAlertFreq, input.MinLDRAlert, input.LDRAlertFreq, input.Connection
            );

            return ObjectMapper.Map<Device, DeviceDto>(device);
        }
    }
}