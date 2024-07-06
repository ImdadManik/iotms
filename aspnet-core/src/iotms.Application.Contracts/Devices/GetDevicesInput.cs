using Volo.Abp.Application.Dtos;
using System;

namespace iotms.Devices
{
    public abstract class GetDevicesInputBase : PagedAndSortedResultRequestDto
    {
        public Guid? AccountId { get; set; }

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public bool? Status { get; set; }
        public bool? Temp { get; set; }
        public bool? LDR { get; set; }
        public bool? PIR { get; set; }
        public bool? Door { get; set; }
        public short? MinTempAlertMin { get; set; }
        public short? MinTempAlertMax { get; set; }
        public short? TempAlertFreqMin { get; set; }
        public short? TempAlertFreqMax { get; set; }
        public short? MinLDRAlertMin { get; set; }
        public short? MinLDRAlertMax { get; set; }
        public short? LDRAlertFreqMin { get; set; }
        public short? LDRAlertFreqMax { get; set; }
        public bool? Connection { get; set; }

        public GetDevicesInputBase()
        {

        }
    }
}