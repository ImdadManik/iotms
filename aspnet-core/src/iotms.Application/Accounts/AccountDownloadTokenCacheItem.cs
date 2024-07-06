using System;

namespace iotms.Accounts;

public abstract class AccountDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}