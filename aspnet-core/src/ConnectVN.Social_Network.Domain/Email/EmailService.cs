using ConnectVN.Social_Network.Admin.Setting;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Security.Encryption;

namespace ConnectVN.Social_Network.Email
{
    public class EmailService : ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        public IStringEncryptionService _encryptionService { get; set; }
        public EmailService(IEmailSender emailSender, IStringEncryptionService encryptionService)
        {
            _emailSender = emailSender;
            _encryptionService = encryptionService;
        }
        public async Task SendEmailAsync()
        {
            //jxLzUxGfQddyJTDKPw+gJA3jY4sqCEcuE/a+i5MvqtQ=
            String encryptedGoogleAppPassword = _encryptionService.Encrypt(MainSetting.ENV_APP_GOOGLE_PASSWORD);
            await _emailSender.SendAsync("leanhphi1706@gmail.com", "Comfirm Register", "Confirm register");
        }
    }
}
