using ConnectVN.Social_Network.Admin.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ConnectVN.Social_Network.Admin.StoryContract
{
    public interface IStoryService : ICrudAppService<
        StoryDTO,
        Guid,
        PagedResultRequestDto,
        CreateUpdateStory>
    {
        /// <summary>
        /// Get list story and paging it by keyword 
        /// </summary>
        /// <param name="baseFilter">Keyword and page | pagesize</param>
        /// <returns>List story</returns>
        Task<PagedResultDto<StoryDTO>> GetListFilterStoryAsync(BaseFilterDto baseFilter);
        /// <summary>
        /// Get list story condition showhome == true
        /// </summary>
        /// <returns>List story</returns>
        Task<PagedResultDto<StoryDTO>> GetListAllStoryAsync();
        /// <summary>
        /// Delete multiple story by list guidId story
        /// </summary>
        /// <param name="guidId">list guidId story</param>
        /// <returns>number story deleted</returns>
        Task DeleteMultipleStoryAsync(IEnumerable<Guid> guidId);
    }
}
