using Asp.Versioning;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using iotms.Accounts;

namespace iotms.Controllers.Accounts
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Account")]
    [Route("api/app/accounts")]

    public class AccountController : AccountControllerBase, IAccountsAppService
    {
        public AccountController(IAccountsAppService accountsAppService) : base(accountsAppService)
        {
        }
    }
}