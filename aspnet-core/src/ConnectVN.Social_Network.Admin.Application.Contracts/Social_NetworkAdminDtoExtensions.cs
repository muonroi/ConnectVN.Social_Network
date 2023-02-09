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
