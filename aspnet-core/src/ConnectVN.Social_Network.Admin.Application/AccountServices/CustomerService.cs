using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.Account.Emailing;
using Volo.Abp.Identity;
using Volo.Abp.Account;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.ObjectExtending;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace ConnectVN.Social_Network.Admin.AccountServices
{
    public class CustomerService : AccountAppService
    {
        public CustomerService(IdentityUserManager userManager,
        IIdentityRoleRepository roleRepository,
        IAccountEmailer accountEmailer,
        IdentitySecurityLogManager identitySecurityLogManager,
        IOptions<IdentityOptions> identityOptions) : base(userManager, roleRepository, accountEmailer, identitySecurityLogManager, identityOptions)
        {

        }
        public async override Task<IdentityUserDto> RegisterAsync(RegisterDto input)
        {
            await CheckSelfRegistrationAsync();

            await IdentityOptions.SetAsync();

            var user = new IdentityUser(GuidGenerator.Create(), input.UserName, input.EmailAddress, CurrentTenant.Id);

            input.MapExtraPropertiesTo(user);

            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();

            await UserManager.SetEmailAsync(user, input.EmailAddress);
            await UserManager.AddDefaultRolesAsync(user);

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }
        public override Task ResetPasswordAsync(ResetPasswordDto input)
        {
            return base.ResetPasswordAsync(input);
        }
        public override Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input)
        {
            return base.SendPasswordResetCodeAsync(input);
        }
    }
}
