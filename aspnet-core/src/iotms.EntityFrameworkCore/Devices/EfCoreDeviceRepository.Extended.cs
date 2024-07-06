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
    public class EfCoreDeviceRepository : EfCoreDeviceRepositoryBase, IDeviceRepository
    {
        public EfCoreDeviceRepository(IDbContextProvider<iotmsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}