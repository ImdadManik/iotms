using Volo.Abp.Application.Dtos;
using System;

namespace iotms.Accounts
{
    public abstract class AccountExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Web { get; set; }
        public int? RoomsMin { get; set; }
        public int? RoomsMax { get; set; }
        public bool? Status { get; set; }

        public AccountExcelDownloadDtoBase()
        {

        }
    }
}