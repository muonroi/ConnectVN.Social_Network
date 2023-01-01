using ConnectVN.Social_Network.Admin.DTO;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Admin.Infrastructure.Services
{
    [Headers("Content-Type: application/json")]
    public interface IUserServiceAPI
    {
        [Post("/account/register")]
        Task<IApiResponse<IdentityUserDto>> RegisterAsync([Body] UserSigupDTO userSigupDTO);
    }
}
