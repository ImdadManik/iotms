using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;

namespace iotms.Devices
{
    public abstract class DeviceDtoBase : FullAuditedEntityDto<Guid>
    {
        public Guid AccountId { get; set; }
        public string Name { get; set; } = null!;
        public bool Status { get; set; }
        public bool Temp { get; set; }
        public bool LDR { get; set; }
        public bool PIR { get; set; }
        public bool Door { get; set; }
        public short MinTempAlert { get; set; }
        public short TempAlertFreq { get; set; }
        public short MinLDRAlert { get; set; }
        public short LDRAlertFreq { get; set; }
        public bool Connection { get; set; }

    }
}