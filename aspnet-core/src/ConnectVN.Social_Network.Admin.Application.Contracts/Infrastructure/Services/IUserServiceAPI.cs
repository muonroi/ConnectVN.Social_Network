using ConnectVN.Social_Network.Admin.DTO;
using Refit;
using System;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Admin.Infrastructure.Services
{
    public interface IUserServiceAPI
    {
        [Post("/account/register")]
        Task<IApiResponse<IdentityUserDto>> RegisterAsync([Body] UserSigupDTO userSigupDTO);

        [Multipart]
        [Post("/app/user/upload-img")]
        Task<IApiResponse<string>> UploadFileImg([AliasAs("files")] StreamPart streams);

        [Delete("/app/user/img")]
        Task<IApiResponse<bool>> DeleteFileImg([AliasAs("guidStory")] Guid guidStory);
    }
}
