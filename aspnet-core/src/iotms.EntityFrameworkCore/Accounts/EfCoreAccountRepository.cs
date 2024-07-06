using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using iotms.EntityFrameworkCore;

namespace iotms.Accounts
{
    public abstract class EfCoreAccountRepositoryBase : EfCoreRepository<iotmsDbContext, Account, Guid>
    {
        public EfCoreAccountRepositoryBase(IDbContextProvider<iotmsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<Account>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? location = null,
            string? address = null,
            string? contact = null,
            string? email = null,
            string? web = null,
            int? roomsMin = null,
            int? roomsMax = null,
            bool? status = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, location, address, contact, email, web, roomsMin, roomsMax, status);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AccountConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? location = null,
            string? address = null,
            string? contact = null,
            string? email = null,
            string? web = null,
            int? roomsMin = null,
            int? roomsMax = null,
            bool? status = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, location, address, contact, email, web, roomsMin, roomsMax, status);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Account> ApplyFilter(
            IQueryable<Account> query,
            string? filterText = null,
            string? name = null,
            string? location = null,
            string? address = null,
            string? contact = null,
            string? email = null,
            string? web = null,
            int? roomsMin = null,
            int? roomsMax = null,
            bool? status = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Location!.Contains(filterText!) || e.Address!.Contains(filterText!) || e.Contact!.Contains(filterText!) || e.Email!.Contains(filterText!) || e.Web!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(location), e => e.Location.Contains(location))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Address.Contains(address))
                    .WhereIf(!string.IsNullOrWhiteSpace(contact), e => e.Contact.Contains(contact))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email.Contains(email))
                    .WhereIf(!string.IsNullOrWhiteSpace(web), e => e.Web.Contains(web))
                    .WhereIf(roomsMin.HasValue, e => e.Rooms >= roomsMin!.Value)
                    .WhereIf(roomsMax.HasValue, e => e.Rooms <= roomsMax!.Value)
                    .WhereIf(status.HasValue, e => e.Status == status);
        }
    }
}