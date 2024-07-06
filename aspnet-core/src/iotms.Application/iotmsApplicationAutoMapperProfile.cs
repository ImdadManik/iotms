using iotms.Devices;
using System;
using iotms.Shared;
using Volo.Abp.AutoMapper;
using iotms.Accounts;
using AutoMapper;

namespace iotms;

public class iotmsApplicationAutoMapperProfile : Profile
{
    public iotmsApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Account, AccountDto>().Ignore(x => x.Devices);
        CreateMap<Account, AccountExcelDto>();

        CreateMap<Device, DeviceDto>();
    }
}