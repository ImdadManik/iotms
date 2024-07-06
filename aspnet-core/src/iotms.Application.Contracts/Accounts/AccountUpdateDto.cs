using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace iotms.Accounts
{
    public abstract class AccountUpdateDtoBase : IHasConcurrencyStamp
    {
        [StringLength(AccountConsts.NameMaxLength)]
        public string? Name { get; set; }
        [StringLength(AccountConsts.LocationMaxLength)]
        public string? Location { get; set; }
        [StringLength(AccountConsts.AddressMaxLength)]
        public string? Address { get; set; }
        [RegularExpression(@"^[0-9]+$")]
        public string? Contact { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [StringLength(AccountConsts.WebMaxLength)]
        public string? Web { get; set; }
        public int Rooms { get; set; }
        public bool Status { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}