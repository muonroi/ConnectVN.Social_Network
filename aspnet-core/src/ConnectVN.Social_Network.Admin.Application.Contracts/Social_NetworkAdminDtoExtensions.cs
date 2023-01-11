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
            //   ObjectExtensionManager.Instance.AddOrUpdate<RegisterDto>(options =>
            //    options.AddOrUpdateProperty<string>("PhoneNumber", options =>
            //    {
            //        options.Attributes.Add(new RequiredAttribute());
            //        options.Attributes.Add(
            //            new StringLengthAttribute(20)
            //            {
            //                MinimumLength = 10
            //            }
            //        );
            //    })
            //);
            //   ObjectExtensionManager.Instance.AddOrUpdate<RegisterDto>(options =>
            //     options.AddOrUpdateProperty<string>("Address", options =>
            //     {
            //         options.Attributes.Add(new RequiredAttribute());
            //         options.Attributes.Add(
            //             new StringLengthAttribute(200)
            //             {
            //                 MinimumLength = 10
            //             }
            //         );
            //     })
            // );
            //   ObjectExtensionManager.Instance.AddOrUpdate<RegisterDto>(options =>
            //    options.AddOrUpdateProperty<DateTime>("BirthDate", options =>
            //    {
            //        options.Validators.Add(
            //            context =>
            //            {
            //                DateTime birthDate = Convert.ToDateTime(context.Value);
            //                DateTime currentTime = DateTime.Now;
            //                if (birthDate.Year > DateTime.Now.Year || birthDate.Year < 1970 || (currentTime.Year - birthDate.Year) <= 10)
            //                {
            //                    context.ValidationErrors.Add(new ValidationResult(
            //                        GetErrorMessage.GetMessage(nameof(EnumUserErrorCodes.USRC34C)), new[] { "Ngày sinh" }
            //                        ));
            //                }
            //            }
            //        );
            //    })
            //);
            //   ObjectExtensionManager.Instance.AddOrUpdate<RegisterDto>(option =>
            //    option.AddOrUpdateProperty<EnumGender>("Gender", options =>
            //    {
            //        options.Validators.Add(
            //            context =>
            //            {
            //                bool success = Enum.IsDefined(typeof(EnumGender), (int)(long)context.Value);
            //                if (!success)
            //                {
            //                    context.ValidationErrors.Add(new ValidationResult(
            //                        GetErrorMessage.GetMessage(nameof(EnumUserErrorCodes.USRC35C)), new[] { "Giới tính" }
            //                        ));
            //                }
            //            }
            //        );
            //    })
            //);
        });
    }
}
