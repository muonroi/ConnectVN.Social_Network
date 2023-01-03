using ConnectVN.Social_Network.Admin.DTO.UserMembers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Admin.UserContract
{
    public interface IUserService : ICrudAppService<UserMemberDTO, Guid, PagedResultRequestDto,
        RegisterUpdateUser>
    {
        /// <summary>
        /// Upload img to minio server
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<string> UploadImg([FromForm] IFormFile files);
        /// <summary>
        /// Get img from server
        /// </summary>
        /// <param name="guidStory"></param>
        /// <returns></returns>

        Task<FileResult> GetImg(Guid guidStory);
        /// <summary>
        /// Remove img from server
        /// </summary>
        /// <param name="guidStory"></param>
        /// <returns></returns>
        Task<bool> DeleteImg(Guid guidStory);
    }
}
