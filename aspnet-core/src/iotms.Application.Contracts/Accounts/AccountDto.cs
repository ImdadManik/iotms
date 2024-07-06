using System;
using System.Collections.Generic;
using iotms.Devices;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace iotms.Accounts
{
    public abstract class AccountDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Web { get; set; }
        public int Rooms { get; set; }
        public bool Status { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

        public List<DeviceDto> Devices { get; set; } = new();
    }
}