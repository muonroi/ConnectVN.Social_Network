using ConnectVN.Social_Network.Email;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Account.Web.Pages.Account;

namespace ConnectVN.Social_Network.Admin.Page.Account
{
    public class CustomRegisterModel : RegisterModel
    {
        private readonly EmailService _emailService;

        public CustomRegisterModel(IAccountAppService accountAppService, EmailService emailService) : base(accountAppService) => _emailService = emailService;

        protected override async Task RegisterLocalUserAsync()
        {
            await _emailService.SendEmailAsync();
            await base.RegisterLocalUserAsync();
        }
    }
}
