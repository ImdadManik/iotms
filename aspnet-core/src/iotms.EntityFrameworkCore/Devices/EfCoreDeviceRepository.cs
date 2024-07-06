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

namespace iotms.Devices
{
    public abstract class EfCoreDeviceRepositoryBase : EfCoreRepository<iotmsDbContext, Device, Guid>
    {
        public EfCoreDeviceRepositoryBase(IDbContextProvider<iotmsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<Device>> GetListByAccountIdAsync(
           Guid accountId,
           string? sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default)
        {
            var query = (await GetQueryableAsync()).Where(x => x.AccountId == accountId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DeviceConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync()).Where(x => x.AccountId == accountId).CountAsync(cancellationToken);
        }

        public virtual async Task<List<Device>> GetListAsync(
            string? filterText = null,
            string? name = null,
            bool? status = null,
            bool? temp = null,
            bool? lDR = null,
            bool? pIR = null,
            bool? door = null,
            short? minTempAlertMin = null,
            short? minTempAlertMax = null,
            short? tempAlertFreqMin = null,
            short? tempAlertFreqMax = null,
            short? minLDRAlertMin = null,
            short? minLDRAlertMax = null,
            short? lDRAlertFreqMin = null,
            short? lDRAlertFreqMax = null,
            bool? connection = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, status, temp, lDR, pIR, door, minTempAlertMin, minTempAlertMax, tempAlertFreqMin, tempAlertFreqMax, minLDRAlertMin, minLDRAlertMax, lDRAlertFreqMin, lDRAlertFreqMax, connection);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DeviceConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            bool? status = null,
            bool? temp = null,
            bool? lDR = null,
            bool? pIR = null,
            bool? door = null,
            short? minTempAlertMin = null,
            short? minTempAlertMax = null,
            short? tempAlertFreqMin = null,
            short? tempAlertFreqMax = null,
            short? minLDRAlertMin = null,
            short? minLDRAlertMax = null,
            short? lDRAlertFreqMin = null,
            short? lDRAlertFreqMax = null,
            bool? connection = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, status, temp, lDR, pIR, door, minTempAlertMin, minTempAlertMax, tempAlertFreqMin, tempAlertFreqMax, minLDRAlertMin, minLDRAlertMax, lDRAlertFreqMin, lDRAlertFreqMax, connection);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Device> ApplyFilter(
            IQueryable<Device> query,
            string? filterText = null,
            string? name = null,
            bool? status = null,
            bool? temp = null,
            bool? lDR = null,
            bool? pIR = null,
            bool? door = null,
            short? minTempAlertMin = null,
            short? minTempAlertMax = null,
            short? tempAlertFreqMin = null,
            short? tempAlertFreqMax = null,
            short? minLDRAlertMin = null,
            short? minLDRAlertMax = null,
            short? lDRAlertFreqMin = null,
            short? lDRAlertFreqMax = null,
            bool? connection = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(status.HasValue, e => e.Status == status)
                    .WhereIf(temp.HasValue, e => e.Temp == temp)
                    .WhereIf(lDR.HasValue, e => e.LDR == lDR)
                    .WhereIf(pIR.HasValue, e => e.PIR == pIR)
                    .WhereIf(door.HasValue, e => e.Door == door)
                    .WhereIf(minTempAlertMin.HasValue, e => e.MinTempAlert >= minTempAlertMin!.Value)
                    .WhereIf(minTempAlertMax.HasValue, e => e.MinTempAlert <= minTempAlertMax!.Value)
                    .WhereIf(tempAlertFreqMin.HasValue, e => e.TempAlertFreq >= tempAlertFreqMin!.Value)
                    .WhereIf(tempAlertFreqMax.HasValue, e => e.TempAlertFreq <= tempAlertFreqMax!.Value)
                    .WhereIf(minLDRAlertMin.HasValue, e => e.MinLDRAlert >= minLDRAlertMin!.Value)
                    .WhereIf(minLDRAlertMax.HasValue, e => e.MinLDRAlert <= minLDRAlertMax!.Value)
                    .WhereIf(lDRAlertFreqMin.HasValue, e => e.LDRAlertFreq >= lDRAlertFreqMin!.Value)
                    .WhereIf(lDRAlertFreqMax.HasValue, e => e.LDRAlertFreq <= lDRAlertFreqMax!.Value)
                    .WhereIf(connection.HasValue, e => e.Connection == connection);
        }
    }
}