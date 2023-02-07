using ConnectVN.Social_Network.Admin.Infrastructure.Extentions;
using ConnectVN.Social_Network.User;
using ConnectVN.Social_Network.Users;
using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace ConnectVN.Social_Network.Admin;

public static class Social_NetworkAdminDtoExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {

        });
    }
}
