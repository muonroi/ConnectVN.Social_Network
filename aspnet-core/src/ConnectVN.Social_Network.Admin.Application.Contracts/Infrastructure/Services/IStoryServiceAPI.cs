using ConnectVN.Social_Network.Admin.DTO;
using ConnectVN.Social_Network.Admin.DTO.Storys;
using Refit;
using System;
using System.Threading.Tasks;

namespace ConnectVN.Social_Network.Admin.Infrastructure.Services
{
    public interface IStoryServiceAPI
    {
        [Multipart]
        [Post("/app/story/upload-img")]
        Task<IApiResponse<string>> UploadFileImg([AliasAs("files")] StreamPart streams);

        [Delete("/app/story/img")]
        Task<IApiResponse<bool>> DeleteFileImg([AliasAs("guidStory")] Guid guidStory);

        [Post("/app/story/model-notifi-cation")]
        Task SendNotification([Body] StoryNotifiCationDTO storyNotifiCationDTO);
    }
}
