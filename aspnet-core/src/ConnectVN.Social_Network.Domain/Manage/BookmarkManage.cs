using ConnectVN.Social_Network.Admin.DTO.Bookmarks;
using ConnectVN.Social_Network.Storys;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Manage
{
    public class BookmarkManage : DomainService
    {
        private readonly IRepository<IdentityUser> _appUserRepository;
        private readonly IRepository<Story> _storyRepository;
        public BookmarkManage(IRepository<IdentityUser> appUserRepository, IRepository<Story> storyRepository)
        {
            _appUserRepository = appUserRepository;
            _storyRepository = storyRepository;
        }
        public async Task<BookmarkDTO> CreateBookmarkAsync(BookmarkDTO request)
        {
            IdentityUser appUser = await _appUserRepository.GetAsync(x => x.Id.Equals(request.UserGuid));
            Story story = await _storyRepository.GetAsync(x => x.Id.Equals(request.StoryGuid));
            return new BookmarkDTO(story.Id, appUser.Id, System.DateTime.Now);
        }
    }
}
