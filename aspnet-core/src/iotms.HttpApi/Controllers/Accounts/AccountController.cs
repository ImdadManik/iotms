using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using iotms.Accounts;
using Volo.Abp.Content;
using iotms.Shared;

namespace iotms.Controllers.Accounts
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Account")]
    [Route("api/app/accounts")]

    public abstract class AccountControllerBase : AbpController
    {
        protected IAccountsAppService _accountsAppService;

        public AccountControllerBase(IAccountsAppService accountsAppService)
        {
            _accountsAppService = accountsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<AccountDto>> GetListAsync(GetAccountsInput input)
        {
            return _accountsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<AccountDto> GetAsync(Guid id)
        {
            return _accountsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<AccountDto> CreateAsync(AccountCreateDto input)
        {
            return _accountsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<AccountDto> UpdateAsync(Guid id, AccountUpdateDto input)
        {
            return _accountsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _accountsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(AccountExcelDownloadDto input)
        {
            return _accountsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<iotms.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _accountsAppService.GetDownloadTokenAsync();
        }

    }
}