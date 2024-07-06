using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace iotms.Accounts
{
    public partial interface IAccountRepository : IRepository<Account, Guid>
    {
        Task<List<Account>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}