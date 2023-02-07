using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Account.Web.Pages.Account;

namespace ConnectVN.Social_Network.Admin.Page.Account
{
    public class CustomRegisterModel : RegisterModel
    {

        public CustomRegisterModel(IAccountAppService accountAppService) : base(accountAppService) { }

        protected override async Task RegisterLocalUserAsync()
        {
            await base.RegisterLocalUserAsync();
        }
    }
}
