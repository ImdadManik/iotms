using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using iotms.Shared;

namespace iotms.Accounts
{
    public partial interface IAccountsAppService : IApplicationService
    {

        Task<PagedResultDto<AccountDto>> GetListAsync(GetAccountsInput input);

        Task<AccountDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<AccountDto> CreateAsync(AccountCreateDto input);

        Task<AccountDto> UpdateAsync(Guid id, AccountUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(AccountExcelDownloadDto input);

        Task<iotms.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}