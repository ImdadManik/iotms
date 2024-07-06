using System;

namespace iotms.Accounts
{
    public abstract class AccountExcelDtoBase
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Web { get; set; }
        public int Rooms { get; set; }
        public bool Status { get; set; }
    }
}