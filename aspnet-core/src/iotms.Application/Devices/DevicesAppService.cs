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
using iotms.Accounts;
using MQTT_Subscriber;

namespace iotms.Devices
{
    [RemoteService(IsEnabled = false)]
    [Authorize(iotmsPermissions.Devices.Default)]
    public abstract class DevicesAppServiceBase : ApplicationService
    {

        protected IDeviceRepository _deviceRepository;
        protected DeviceManager _deviceManager;

        protected IAccountRepository _accountRepository;
        protected AccountManager _accountManager;
        public DevicesAppServiceBase(IDeviceRepository deviceRepository, DeviceManager deviceManager, IAccountRepository accountRepository, AccountManager accountManager)
        {
            _deviceRepository = deviceRepository;
            _deviceManager = deviceManager;

            _accountRepository = accountRepository;
            _accountManager = accountManager;
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
            var response = cEmqxAPI.DeleteUsers(_device);
            if (response.IsSuccessful || response.StatusDescription == "Not Found")
            {
                //to disconnect the client automatically use topic : device/remove/devicename
                Emqx.cMsgPublisher.PublishMessage("1", _device.Name, $"device/remove/{_device.Name}");
                await _deviceRepository.DeleteAsync(input);
            }
        }

        [Authorize(iotmsPermissions.Devices.Create)]
        public virtual async Task<DeviceDto> CreateAsync(DeviceCreateDto input)
        {
            GenerateAES aes = new GenerateAES("098pub+1key+0pri", 256, "ABCXYZ123098");
            var resp = cEmqxAPI.AddAuthUsers(input.Name, aes.Encrypt(input.Name), false); 

            if (resp.StatusDescription == "Created")
            { 
                var device = await _deviceManager.CreateAsync(input.AccountId,
                input.Name, input.Status, input.Temp, input.LDR, input.PIR, input.Door, input.MinTempAlert, input.TempAlertFreq, input.MinLDRAlert, input.LDRAlertFreq, input.Connection
                );
                
                return ObjectMapper.Map<Device, DeviceDto>(device);
                //string json_payload = MQTT_Subscriber.cMyDAL.GetSettingsPayload(input.Name, input.AccountId.ToString());
                //Emqx.cMsgPublisher.PublishMessage(json_payload, input.Name, "device/" + input.Name);
            }
            else
                return null;
        }

        [Authorize(iotmsPermissions.Devices.Edit)]
        public virtual async Task<DeviceDto> UpdateAsync(Guid id, DeviceUpdateDto input)
        { 
            var device = await _deviceManager.UpdateAsync(id, input.AccountId, input.Name, input.Status, input.Temp, input.LDR, 
                input.PIR, input.Door, input.MinTempAlert, input.TempAlertFreq, input.MinLDRAlert, 
                input.LDRAlertFreq, input.Connection);

            string json_payload = MQTT_Subscriber.cMyDAL.GetSettingsPayload(input.Name, input.AccountId.ToString(), id.ToString());
            SensorPayload sensorPayload = JsonConvert.DeserializeObject<SensorPayload>(json_payload);
            sensorPayload.DeviceStatus = device.Status;
            var payload = JsonConvert.SerializeObject(sensorPayload);
            Emqx.cMsgPublisher.PublishMessage(payload, input.Name, "device/" + input.Name);
            return ObjectMapper.Map<Device, DeviceDto>(device);
        }
    }
}
