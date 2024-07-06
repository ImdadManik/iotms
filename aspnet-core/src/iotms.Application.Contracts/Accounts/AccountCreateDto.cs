using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace iotms.Accounts
{
    public abstract class AccountCreateDtoBase
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
        public bool Status { get; set; } = true;
    }
}