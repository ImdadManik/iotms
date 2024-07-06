using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace iotms.Devices
{
    public abstract class DeviceUpdateDtoBase
    {
        public Guid AccountId { get; set; }
        [Required]
        [StringLength(DeviceConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
        public bool Status { get; set; }
        public bool Temp { get; set; }
        public bool LDR { get; set; }
        public bool PIR { get; set; }
        public bool Door { get; set; }
        [Range(DeviceConsts.MinTempAlertMinLength, DeviceConsts.MinTempAlertMaxLength)]
        public short MinTempAlert { get; set; }
        [Required]
        [Range(DeviceConsts.TempAlertFreqMinLength, DeviceConsts.TempAlertFreqMaxLength)]
        public short TempAlertFreq { get; set; }
        [Range(DeviceConsts.MinLDRAlertMinLength, DeviceConsts.MinLDRAlertMaxLength)]
        public short MinLDRAlert { get; set; }
        [Required]
        [Range(DeviceConsts.LDRAlertFreqMinLength, DeviceConsts.LDRAlertFreqMaxLength)]
        public short LDRAlertFreq { get; set; }
        public bool Connection { get; set; }

    }
}