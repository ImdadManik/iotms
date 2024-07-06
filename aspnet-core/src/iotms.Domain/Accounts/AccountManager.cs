using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace iotms.Accounts
{
    public abstract class AccountManagerBase : DomainService
    {
        protected IAccountRepository _accountRepository;

        public AccountManagerBase(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public virtual async Task<Account> CreateAsync(
        int rooms, bool status, string? name = null, string? location = null, string? address = null, string? contact = null, string? email = null, string? web = null)
        {
            Check.Length(name, nameof(name), AccountConsts.NameMaxLength);
            Check.Length(location, nameof(location), AccountConsts.LocationMaxLength);
            Check.Length(address, nameof(address), AccountConsts.AddressMaxLength);
            Check.Length(web, nameof(web), AccountConsts.WebMaxLength);

            var account = new Account(
             GuidGenerator.Create(),
             rooms, status, name, location, address, contact, email, web
             );

            return await _accountRepository.InsertAsync(account);
        }

        public virtual async Task<Account> UpdateAsync(
            Guid id,
            int rooms, bool status, string? name = null, string? location = null, string? address = null, string? contact = null, string? email = null, string? web = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.Length(name, nameof(name), AccountConsts.NameMaxLength);
            Check.Length(location, nameof(location), AccountConsts.LocationMaxLength);
            Check.Length(address, nameof(address), AccountConsts.AddressMaxLength);
            Check.Length(web, nameof(web), AccountConsts.WebMaxLength);

            var account = await _accountRepository.GetAsync(id);

            account.Rooms = rooms;
            account.Status = status;
            account.Name = name;
            account.Location = location;
            account.Address = address;
            account.Contact = contact;
            account.Email = email;
            account.Web = web;

            account.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _accountRepository.UpdateAsync(account);
        }

    }
}