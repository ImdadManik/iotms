using Volo.Abp.Application.Dtos;
using System;

namespace iotms.Devices
{
    public class GetDeviceListInput : PagedAndSortedResultRequestDto
    {
        public Guid AccountId { get; set; }
    }
}