using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Core;
using ConnectVN.Social_Network.Admin.DTO;
using ConnectVN.Social_Network.Admin.Email;
using ConnectVN.Social_Network.Admin.Infrastructure.Services;
using ConnectVN.Social_Network.Admin.MailServices;
using ConnectVN.Social_Network.Admin.Setting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Refit;
using Serilog;
using Serilog.Events;

namespace ConnectVN.Social_Network.Admin;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting ConnectVN.Social_Network.Admin.HttpApi.Host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRefitClient<IUserServiceAPI>().ConfigureHttpClient(s =>
            {
                s.BaseAddress = new Uri(Environment.GetEnvironmentVariable(MainSetting.ENV_USER_SERVICE_API_URL));
            });
            builder.Services.AddRefitClient<IStoryServiceAPI>().ConfigureHttpClient(s =>
            {
                s.BaseAddress = new Uri(Environment.GetEnvironmentVariable(MainSetting.ENV_USER_SERVICE_API_URL));
            });
            builder.Services.AddScoped<IEmailService, MailService>();

            builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<Social_NetworkAdminHttpApiHostModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }

    }

}
