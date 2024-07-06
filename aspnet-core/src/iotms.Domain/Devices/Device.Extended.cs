using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace iotms.Devices
{
    public class Device : DeviceBase
    {
        //<suite-custom-code-autogenerated>
        protected Device()
        {

        }

        public Device(Guid id, Guid accountId, string name, bool status, bool temp, bool lDR, bool pIR, bool door, short minTempAlert, short tempAlertFreq, short minLDRAlert, short lDRAlertFreq, bool connection)
            : base(id, accountId, name, status, temp, lDR, pIR, door, minTempAlert, tempAlertFreq, minLDRAlert, lDRAlertFreq, connection)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}