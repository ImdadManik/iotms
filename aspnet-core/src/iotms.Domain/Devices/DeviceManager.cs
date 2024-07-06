using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace iotms.Devices
{
    public abstract class DeviceManagerBase : DomainService
    {
        protected IDeviceRepository _deviceRepository;

        public DeviceManagerBase(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public virtual async Task<Device> CreateAsync(
        Guid accountId, string name, bool status, bool temp, bool lDR, bool pIR, bool door, short minTempAlert, short tempAlertFreq, short minLDRAlert, short lDRAlertFreq, bool connection)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DeviceConsts.NameMaxLength);
            Check.Range(minTempAlert, nameof(minTempAlert), DeviceConsts.MinTempAlertMinLength, DeviceConsts.MinTempAlertMaxLength);
            Check.Range(tempAlertFreq, nameof(tempAlertFreq), DeviceConsts.TempAlertFreqMinLength, DeviceConsts.TempAlertFreqMaxLength);
            Check.Range(minLDRAlert, nameof(minLDRAlert), DeviceConsts.MinLDRAlertMinLength, DeviceConsts.MinLDRAlertMaxLength);
            Check.Range(lDRAlertFreq, nameof(lDRAlertFreq), DeviceConsts.LDRAlertFreqMinLength, DeviceConsts.LDRAlertFreqMaxLength);

            var device = new Device(
             GuidGenerator.Create(),
             accountId, name, status, temp, lDR, pIR, door, minTempAlert, tempAlertFreq, minLDRAlert, lDRAlertFreq, connection
             );

            return await _deviceRepository.InsertAsync(device);
        }

        public virtual async Task<Device> UpdateAsync(
            Guid id,
            Guid accountId, string name, bool status, bool temp, bool lDR, bool pIR, bool door, short minTempAlert, short tempAlertFreq, short minLDRAlert, short lDRAlertFreq, bool connection
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), DeviceConsts.NameMaxLength);
            Check.Range(minTempAlert, nameof(minTempAlert), DeviceConsts.MinTempAlertMinLength, DeviceConsts.MinTempAlertMaxLength);
            Check.Range(tempAlertFreq, nameof(tempAlertFreq), DeviceConsts.TempAlertFreqMinLength, DeviceConsts.TempAlertFreqMaxLength);
            Check.Range(minLDRAlert, nameof(minLDRAlert), DeviceConsts.MinLDRAlertMinLength, DeviceConsts.MinLDRAlertMaxLength);
            Check.Range(lDRAlertFreq, nameof(lDRAlertFreq), DeviceConsts.LDRAlertFreqMinLength, DeviceConsts.LDRAlertFreqMaxLength);

            var device = await _deviceRepository.GetAsync(id);

            device.AccountId = accountId;
            device.Name = name;
            device.Status = status;
            device.Temp = temp;
            device.LDR = lDR;
            device.PIR = pIR;
            device.Door = door;
            device.MinTempAlert = minTempAlert;
            device.TempAlertFreq = tempAlertFreq;
            device.MinLDRAlert = minLDRAlert;
            device.LDRAlertFreq = lDRAlertFreq;
            device.Connection = connection;

            return await _deviceRepository.UpdateAsync(device);
        }

    }
}