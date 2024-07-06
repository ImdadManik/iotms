using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace iotms.Devices
{
    public partial interface IDeviceRepository : IRepository<Device, Guid>
    {
        Task<List<Device>> GetListByAccountIdAsync(
    Guid accountId,
    string? sorting = null,
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    CancellationToken cancellationToken = default
);

        Task<long> GetCountByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default);

        Task<List<Device>> GetListAsync(
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
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}