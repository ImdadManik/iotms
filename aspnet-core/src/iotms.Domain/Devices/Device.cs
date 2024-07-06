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
    public abstract class DeviceBase : FullAuditedEntity<Guid>
    {
        public virtual Guid AccountId { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        public virtual bool Status { get; set; }

        public virtual bool Temp { get; set; }

        public virtual bool LDR { get; set; }

        public virtual bool PIR { get; set; }

        public virtual bool Door { get; set; }

        public virtual short MinTempAlert { get; set; }

        public virtual short TempAlertFreq { get; set; }

        public virtual short MinLDRAlert { get; set; }

        public virtual short LDRAlertFreq { get; set; }

        public virtual bool Connection { get; set; }

        protected DeviceBase()
        {

        }

        public DeviceBase(Guid id, Guid accountId, string name, bool status, bool temp, bool lDR, bool pIR, bool door, short minTempAlert, short tempAlertFreq, short minLDRAlert, short lDRAlertFreq, bool connection)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), DeviceConsts.NameMaxLength, 0);
            if (minTempAlert < DeviceConsts.MinTempAlertMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(minTempAlert), minTempAlert, "The value of 'minTempAlert' cannot be lower than " + DeviceConsts.MinTempAlertMinLength);
            }

            if (minTempAlert > DeviceConsts.MinTempAlertMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(minTempAlert), minTempAlert, "The value of 'minTempAlert' cannot be greater than " + DeviceConsts.MinTempAlertMaxLength);
            }

            if (tempAlertFreq < DeviceConsts.TempAlertFreqMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(tempAlertFreq), tempAlertFreq, "The value of 'tempAlertFreq' cannot be lower than " + DeviceConsts.TempAlertFreqMinLength);
            }

            if (tempAlertFreq > DeviceConsts.TempAlertFreqMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(tempAlertFreq), tempAlertFreq, "The value of 'tempAlertFreq' cannot be greater than " + DeviceConsts.TempAlertFreqMaxLength);
            }

            if (minLDRAlert < DeviceConsts.MinLDRAlertMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(minLDRAlert), minLDRAlert, "The value of 'minLDRAlert' cannot be lower than " + DeviceConsts.MinLDRAlertMinLength);
            }

            if (minLDRAlert > DeviceConsts.MinLDRAlertMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(minLDRAlert), minLDRAlert, "The value of 'minLDRAlert' cannot be greater than " + DeviceConsts.MinLDRAlertMaxLength);
            }

            if (lDRAlertFreq < DeviceConsts.LDRAlertFreqMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(lDRAlertFreq), lDRAlertFreq, "The value of 'lDRAlertFreq' cannot be lower than " + DeviceConsts.LDRAlertFreqMinLength);
            }

            if (lDRAlertFreq > DeviceConsts.LDRAlertFreqMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(lDRAlertFreq), lDRAlertFreq, "The value of 'lDRAlertFreq' cannot be greater than " + DeviceConsts.LDRAlertFreqMaxLength);
            }

            AccountId = accountId;
            Name = name;
            Status = status;
            Temp = temp;
            LDR = lDR;
            PIR = pIR;
            Door = door;
            MinTempAlert = minTempAlert;
            TempAlertFreq = tempAlertFreq;
            MinLDRAlert = minLDRAlert;
            LDRAlertFreq = lDRAlertFreq;
            Connection = connection;
        }

    }
}