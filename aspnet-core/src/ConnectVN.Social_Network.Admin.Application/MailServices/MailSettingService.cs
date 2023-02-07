using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Volo.Abp.Emailing;
using Volo.Abp.SettingManagement;

namespace ConnectVN.Social_Network.Admin.MailServices
{
    [AllowAnonymous]
    public class MailSettingService : EmailSettingsAppService
    {
        public MailSettingService(ISettingManager settingManager, IEmailSender emailSender) : base(settingManager, emailSender)
        { }
        [AllowAnonymous]
        public override Task<EmailSettingsDto> GetAsync()
        {
            return base.GetAsync();
        }
        [AllowAnonymous]
        public async override Task UpdateAsync(UpdateEmailSettingsDto input)
        {
            await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, EmailSettingNames.Smtp.Host, input.SmtpHost);
            await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, EmailSettingNames.Smtp.Port, input.SmtpPort.ToString());
            await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, EmailSettingNames.Smtp.UserName, input.SmtpUserName);
            await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, EmailSettingNames.Smtp.Password, input.SmtpPassword);
            await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, EmailSettingNames.Smtp.Domain, input.SmtpDomain);
            await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, EmailSettingNames.Smtp.EnableSsl, input.SmtpEnableSsl.ToString());
            await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, EmailSettingNames.Smtp.UseDefaultCredentials, input.SmtpUseDefaultCredentials.ToString().ToLowerInvariant());
            await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, EmailSettingNames.DefaultFromAddress, input.DefaultFromAddress);
            await SettingManager.SetForTenantOrGlobalAsync(CurrentTenant.Id, EmailSettingNames.DefaultFromDisplayName, input.DefaultFromDisplayName);
        }

        [AllowAnonymous]
        public override Task SendTestEmailAsync(SendTestEmailInput input)
        {
            return base.SendTestEmailAsync(input);
        }
    }
}
