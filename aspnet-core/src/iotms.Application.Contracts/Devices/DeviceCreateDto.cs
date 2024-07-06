using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace iotms.Devices
{
    public abstract class DeviceCreateDtoBase
    {
        public Guid AccountId { get; set; }
        [Required]
        [StringLength(DeviceConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
        public bool Status { get; set; } = true;
        public bool Temp { get; set; } = true;
        public bool LDR { get; set; } = true;
        public bool PIR { get; set; } = true;
        public bool Door { get; set; } = true;
        [Range(DeviceConsts.MinTempAlertMinLength, DeviceConsts.MinTempAlertMaxLength)]
        public short MinTempAlert { get; set; } = 20;
        [Required]
        [Range(DeviceConsts.TempAlertFreqMinLength, DeviceConsts.TempAlertFreqMaxLength)]
        public short TempAlertFreq { get; set; } = 30;
        [Range(DeviceConsts.MinLDRAlertMinLength, DeviceConsts.MinLDRAlertMaxLength)]
        public short MinLDRAlert { get; set; }
        [Required]
        [Range(DeviceConsts.LDRAlertFreqMinLength, DeviceConsts.LDRAlertFreqMaxLength)]
        public short LDRAlertFreq { get; set; }
        public bool Connection { get; set; } = false;
    }
}