using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using iotms.Permissions;
using iotms.Accounts;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using iotms.Shared;

namespace iotms.Accounts
{
    [RemoteService(IsEnabled = false)]
    [Authorize(iotmsPermissions.Accounts.Default)]
    public abstract class AccountsAppServiceBase : iotmsAppService
    {
        protected IDistributedCache<AccountDownloadTokenCacheItem, string> _downloadTokenCache;
        protected IAccountRepository _accountRepository;
        protected AccountManager _accountManager;

        public AccountsAppServiceBase(IAccountRepository accountRepository, AccountManager accountManager, IDistributedCache<AccountDownloadTokenCacheItem, string> downloadTokenCache)
        {
            _downloadTokenCache = downloadTokenCache;
            _accountRepository = accountRepository;
            _accountManager = accountManager;

        }

        public virtual async Task<PagedResultDto<AccountDto>> GetListAsync(GetAccountsInput input)
        {
            var totalCount = await _accountRepository.GetCountAsync(input.FilterText, input.Name, input.Location, input.Address, input.Contact, input.Email, input.Web, input.RoomsMin, input.RoomsMax, input.Status);
            var items = await _accountRepository.GetListAsync(input.FilterText, input.Name, input.Location, input.Address, input.Contact, input.Email, input.Web, input.RoomsMin, input.RoomsMax, input.Status, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AccountDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Account>, List<AccountDto>>(items)
            };
        }

        public virtual async Task<AccountDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Account, AccountDto>(await _accountRepository.GetAsync(id));
        }

        [Authorize(iotmsPermissions.Accounts.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _accountRepository.DeleteAsync(id);
        }

        [Authorize(iotmsPermissions.Accounts.Create)]
        public virtual async Task<AccountDto> CreateAsync(AccountCreateDto input)
        {

            var account = await _accountManager.CreateAsync(
            input.Rooms, input.Status, input.Name, input.Location, input.Address, input.Contact, input.Email, input.Web
            );

            return ObjectMapper.Map<Account, AccountDto>(account);
        }

        [Authorize(iotmsPermissions.Accounts.Edit)]
        public virtual async Task<AccountDto> UpdateAsync(Guid id, AccountUpdateDto input)
        {

            var account = await _accountManager.UpdateAsync(
            id,
            input.Rooms, input.Status, input.Name, input.Location, input.Address, input.Contact, input.Email, input.Web, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Account, AccountDto>(account);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(AccountExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _accountRepository.GetListAsync(input.FilterText, input.Name, input.Location, input.Address, input.Contact, input.Email, input.Web, input.RoomsMin, input.RoomsMax, input.Status);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Account>, List<AccountExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Accounts.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<iotms.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new AccountDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new iotms.Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}