using ConnectVN.Social_Network.Admin.BookmarkContract;
using ConnectVN.Social_Network.Admin.DTO.Bookmarks;
using ConnectVN.Social_Network.Manage;
using ConnectVN.Social_Network.Users;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq;
using Volo.Abp.ObjectMapping;
using ConnectVN.Social_Network.Storys;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Admin.Bookmarks
{
    public class BookmarkServices
        : CrudAppService<BookMarkStory, BookmarkDTO, int, PagedResultRequestDto, BookmarkDTO, BookmarkDTO>, IBookmarkService
    {
        private readonly IRepository<IdentityUser> _appUserRepository;
        private readonly BookmarkManage _bookmarkManage;
        public BookmarkServices(IRepository<BookMarkStory, int> repository, BookmarkManage bookmarkManage, IRepository<IdentityUser> appUserRepository) : base(repository)
        {
            _bookmarkManage = bookmarkManage;

        }
        /// <summary>
        /// Delete multiple bookmark by list id of bookmark by user
        /// </summary>
        /// <param name="id">id of bookmark delete</param>
        /// <param name="userGuid">User guid</param>
        /// <returns></returns>

        public async Task DeleteMultipleStoryAsync(IEnumerable<int> id, Guid userGuid)
        {
            await _appUserRepository.GetAsync(x => x.Id.Equals(userGuid));
            await Repository.DeleteManyAsync(id);

        }
        // Delete multi story
        /// <summary>
        /// Create new bookmark
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async override Task<BookmarkDTO> CreateAsync(BookmarkDTO input)
        {
            BookmarkDTO bookMarkDto = await _bookmarkManage.CreateBookmarkAsync(input);
            await Repository.InsertAsync(MapToEntity(bookMarkDto));
            return bookMarkDto;
        }
        /// <summary>
        /// Get bookmark by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async override Task<BookmarkDTO> GetAsync(int id)
        {
            BookMarkStory bookMarkStory = await Repository.GetAsync(id);
            await _appUserRepository.GetAsync(x => x.Id.Equals(bookMarkStory.UserGuid));
            return ObjectMapper.Map<BookMarkStory, BookmarkDTO>(bookMarkStory);
        }
        /// <summary>
        /// Delete bookmark by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async override Task DeleteAsync(int id)
        {
            BookMarkStory bookMarkStory = await Repository.GetAsync(id);
            await _appUserRepository.GetAsync(x => x.Id.Equals(bookMarkStory.UserGuid));
            await Repository.DeleteAsync(bookMarkStory);
        }
        /// <summary>
        /// Get all bookmark (no need to use)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<PagedResultDto<BookmarkDTO>> GetListAsync(PagedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }
        /// <summary>
        /// Get all bookmark by user
        /// </summary>
        /// <param name="pagedResultRequestDto"></param>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PagedResultDto<BookmarkDTO>> GetAllBookmark(PagedResultRequestDto pagedResultRequestDto, Guid userGuid)
        {
            PagedResultDto<BookmarkDTO> pagedResultDto = null;
            await _appUserRepository.GetAsync(x => x.Id.Equals(userGuid));
            List<BookmarkDTO> bookmarkDTOs = null;
            IEnumerable<BookMarkStory> listBookmark = await Repository.GetListAsync(x => x.UserGuid.Equals(userGuid));
            listBookmark.ToList().ForEach(x =>
            {
                BookmarkDTO temp = new()
                {
                    StoryGuid = x.StoryGuid,
                    UserGuid = x.UserGuid,
                    Id = x.Id,
                    BookmarkDate = x.BookmarkDate,
                };
                bookmarkDTOs.Add(temp);
            });
            bookmarkDTOs = bookmarkDTOs.Skip(pagedResultRequestDto.SkipCount).Take(pagedResultRequestDto.MaxResultCount).ToList();
            int countBookmark = bookmarkDTOs.Count;
            return pagedResultDto = new(countBookmark, bookmarkDTOs);
        }
    }
}
