using ConnectVN.Social_Network.Admin.DTO.Bookmarks;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ConnectVN.Social_Network.Admin.BookmarkContract
{
    public interface IBookmarkService : ICrudAppService<BookmarkDTO, int, PagedResultRequestDto, BookmarkDTO>
    {
        /// <summary>
        /// Get all bookmark by user
        /// </summary>
        /// <param name="pagedResultRequestDto"></param>
        /// <returns></returns>
        Task<PagedResultDto<BookmarkDTO>> GetAllBookmark(PagedResultRequestDto pagedResultRequestDto, Guid userGuid);
    }
}
