using ConnectVN.Social_Network.Admin.DTO.Storys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ConnectVN.Social_Network.Admin.StoryContract
{
    public interface IStoryService : ICrudAppService<
        DetailStoryDTO,
        Guid,
        PagedResultRequestDto,
        CreateUpdateStoryDTO>
    {
        /// <summary>
        /// Get list story and paging it by keyword 
        /// </summary>
        /// <param name="baseFilter">Keyword and page | pagesize</param>
        /// <returns>List story</returns>
        Task<PagedResultDto<DetailStoryDTO>> GetListFilterStoryAsync(BaseFilterDto baseFilter);
        /// <summary>
        /// Get list story new update
        /// </summary>
        /// <param name="baseFilter">Keyword and page | pagesize</param>
        /// <returns>List story</returns>
        Task<PagedResultDto<DetailStoryDTO>> GetListNewStoryAsync(PagedResultRequestDto baseFilter);
        /// <summary>
        /// Get list story condition showhome == true
        /// </summary>
        /// <returns>List story</returns>
        Task<PagedResultDto<DetailStoryDTO>> GetListAllStoryAsync(PagedResultRequestDto requestPage);
        /// <summary>
        /// Delete multiple story by list guidId story
        /// </summary>
        /// <param name="guidId">list guidId story</param>
        /// <returns>number story deleted</returns>
        Task DeleteMultipleStoryAsync(IEnumerable<Guid> guidId);

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
        /// <summary>
        /// Notification when publish story
        /// </summary>
        /// <param name="storyNotifiCationDTO"></param>
        /// <returns></returns>
        Task CreateModelNotifiCation(StoryNotifiCationDTO storyNotifiCationDTO);
    }
}
