using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Autofac;
using ConnectVN.Social_Network.Admin.Infrastructure.Extentions;
using ConnectVN.Social_Network.Admin.Infrastructure.Services;
using ConnectVN.Social_Network.Admin.Setting;
using ConnectVN.Social_Network.User;
using ConnectVN.Social_Network.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Refit;
using Serilog;
using Serilog.Events;
using Volo.Abp.Account;
using Volo.Abp.ObjectExtending;

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
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            ObjectExtensionManager.Instance.AddOrUpdate<RegisterDto>(options =>
               options.AddOrUpdateProperty<string>("PhoneNumber", options =>
               {
                   options.Attributes.Add(new RequiredAttribute());
                   options.Attributes.Add(
                       new StringLengthAttribute(20)
                       {
                           MinimumLength = 10
                       }
                   );
               })
           );
            ObjectExtensionManager.Instance.AddOrUpdate<RegisterDto>(options =>
              options.AddOrUpdateProperty<string>("Address", options =>
              {
                  options.Attributes.Add(new RequiredAttribute());
                  options.Attributes.Add(
                      new StringLengthAttribute(200)
                      {
                          MinimumLength = 10
                      }
                  );
              })
          );
            ObjectExtensionManager.Instance.AddOrUpdate<RegisterDto>(options =>
             options.AddOrUpdateProperty<DateTime>("BirthDate", options =>
             {
                 options.Validators.Add(
                     context =>
                     {
                         DateTime birthDate = Convert.ToDateTime(context.Value);
                         DateTime currentTime = DateTime.Now;
                         if (birthDate.Year > DateTime.Now.Year || birthDate.Year < 1970 || (currentTime.Year - birthDate.Year) <= 10)
                         {
                             context.ValidationErrors.Add(new ValidationResult(
                                 GetErrorMessage.GetMessage(nameof(EnumUserErrorCodes.USRC34C)), new[] { "Ngày sinh" }
                                 ));
                         }
                     }
                 );
             })
         );
            ObjectExtensionManager.Instance.AddOrUpdate<RegisterDto>(option =>
             option.AddOrUpdateProperty<EnumGender>("Gender", options =>
             {
                 options.Validators.Add(
                     context =>
                     {
                         bool success = Enum.IsDefined(typeof(EnumGender), (int)(long)context.Value);
                         if (!success)
                         {
                             context.ValidationErrors.Add(new ValidationResult(
                                 GetErrorMessage.GetMessage(nameof(EnumUserErrorCodes.USRC35C)), new[] { "Giới tính" }
                                 ));
                         }
                     }
                 );
             })
         );
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
