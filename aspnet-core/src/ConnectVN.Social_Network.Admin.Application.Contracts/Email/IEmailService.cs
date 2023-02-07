using ConnectVN.Social_Network.Users;
using System.Threading.Tasks;

namespace ConnectVN.Social_Network.Admin.Email
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);

        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);

        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);
    }
}
