using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.Account.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.Account;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.ObjectExtending;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using ConnectVN.Social_Network.Users;
using ConnectVN.Social_Network.Admin.Email;
using System;
using ConnectVN.Social_Network.Domain;
using ConnectVN.Social_Network.Tags;
using Volo.Abp;

namespace ConnectVN.Social_Network.Admin.AccountServices
{

    public class CustomerService : AccountAppService
    {
        private readonly UserManager<IdentityUser> _userManagers;

        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public CustomerService(IdentityUserManager userManager,
        IIdentityRoleRepository roleRepository,
        IAccountEmailer accountEmailer,
        IdentitySecurityLogManager identitySecurityLogManager,
        IOptions<IdentityOptions> identityOptions, UserManager<IdentityUser> userManagers, IEmailService emailService, IConfiguration configuration) : base(userManager, roleRepository, accountEmailer, identitySecurityLogManager, identityOptions)
        {
            _emailService = emailService;
            _configuration = configuration;
            _userManagers = userManagers;
        }
        [AllowAnonymous]
        public async override Task<IdentityUserDto> RegisterAsync(RegisterDto input)
        {
            await CheckSelfRegistrationAsync();

            await IdentityOptions.SetAsync();

            IdentityUser user = new(GuidGenerator.Create(), input.UserName, input.EmailAddress, CurrentTenant.Id);
            input.MapExtraPropertiesTo(user);

            IdentityResult result = await UserManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                await GenerateEmailConfirmationTokenAsync(user);
            }
            await UserManager.AddDefaultRolesAsync(user);

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }
        public async Task GenerateEmailConfirmationTokenAsync(IdentityUser identityUser)
        {
            string token = await _userManagers.GenerateEmailConfirmationTokenAsync(identityUser).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(identityUser, token);
            }
        }
        private async Task SendEmailConfirmationEmail(IdentityUser user, string token)
        {
            try
            {
                string appDomain = _configuration.GetSection("Application:AppDomain").Value;
                string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

                UserEmailOptions options = new()
                {
                    ToEmails = new List<string>() { user.Email },
                    PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.UserName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
                };

                await _emailService.SendEmailForEmailConfirmation(options);
            }
            catch (Exception ex)
            {

                throw new BusinessException($"{ex.Message}", Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
            }

        }
        public override Task ResetPasswordAsync(ResetPasswordDto input)
        {
            return base.ResetPasswordAsync(input);
        }
        public override Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input)
        {
            return base.SendPasswordResetCodeAsync(input);
        }
        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManagers.ConfirmEmailAsync(await _userManagers.FindByIdAsync(uid), token);
        }
    }
}
