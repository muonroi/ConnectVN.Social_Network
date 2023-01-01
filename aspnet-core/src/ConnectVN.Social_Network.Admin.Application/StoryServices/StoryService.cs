using ConnectVN.Social_Network.Admin.DTO.Storys;
using ConnectVN.Social_Network.Admin.StoryContract;
using ConnectVN.Social_Network.Categories;
using ConnectVN.Social_Network.Manage;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.Tags;
using ConnectVN.Social_Network.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Security.Encryption;

namespace ConnectVN.Social_Network.Admin.StoryServices
{
    public class StoryService : CrudAppService<Story, DetailStoryDTO, Guid, PagedResultRequestDto, CreateUpdateStoryDTO, CreateUpdateStoryDTO>, IStoryService
    {
        private readonly StoryManage _storyManage;
        private readonly IRepository<TagInStory> _tagInstoryRepository;
        private readonly IRepository<CategoryInStory> _categoryinStoryRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<UserMember> _userMemberRepository;
        protected IStringEncryptionService StringEncryptionService { get; }

        public StoryService(
            IRepository<Story, Guid> repository,
            IRepository<TagInStory> tagInstoryRepository,
            IRepository<CategoryInStory> categoryinStoryRepository,
            IRepository<Tag> tagRepository,
            IRepository<Category> categoryRepository,
            IRepository<UserMember> userMemberRepository, IStringEncryptionService stringEncryptionService
            , StoryManage storyManage) : base(repository)
        {
            _storyManage = storyManage;
            _categoryinStoryRepository = categoryinStoryRepository;
            _tagInstoryRepository = tagInstoryRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _userMemberRepository = userMemberRepository;
            StringEncryptionService = stringEncryptionService;

        }

        public async Task DeleteMultipleStoryAsync(IEnumerable<Guid> guidId)
        {
            await Repository.DeleteManyAsync(guidId);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
        public async override Task<DetailStoryDTO> GetAsync(Guid id)
        {
            Story queryStory = await Repository.GetAsync(id);
            IQueryable<CategoryInStory> queryStoryInCategory = await _categoryinStoryRepository.GetQueryableAsync();
            IQueryable<TagInStory> queryTagInStory = await _tagInstoryRepository.GetQueryableAsync();
            int tagId = queryTagInStory.FirstOrDefault(x => x.StoryGuid.Equals(queryStory.Id)).TagId;
            int CatId = queryStoryInCategory.FirstOrDefault(x => x.StoryGuid.Equals(queryStory.Id)).CategoryId;
            Category queryCategory = await _categoryRepository.GetAsync(x => x.Id == CatId);
            Tag queryTag = await _tagRepository.GetAsync(x => x.Id == tagId);
            UserMember author = await _userMemberRepository.GetAsync(x => x.Id.Equals(queryStory.CreatorId));
            DetailStoryDTO story = new()
            {
                Story_Title = queryStory.Story_Title,
                Story_Synopsis = queryStory.Story_Synopsis,
                Img_Url = queryStory.Img_Url,
                IsShow = queryStory.IsShow,
                NameCategory = queryCategory.NameCategory,
                TagName = queryTag.TagName,
                AuthorName = author.LastName + " " + author.FirstName,
                TotalFavorite = queryStory.TotalFavorite,
                TotalViews = queryStory.TotalView,
            };
            queryStory.TotalView++;
            await Repository.UpdateAsync(queryStory);
            return story;
        }
        public async Task<PagedResultDto<DetailStoryDTO>> GetListAllStoryAsync(PagedResultRequestDto requestPage)
        {
            IQueryable<Story> queryStory = await Repository.GetQueryableAsync();
            IQueryable<CategoryInStory> queryStoryInCategory = await _categoryinStoryRepository.GetQueryableAsync();
            IQueryable<TagInStory> queryTagInStory = await _tagInstoryRepository.GetQueryableAsync();
            IQueryable<Category> queryCategory = await _categoryRepository.GetQueryableAsync();
            IQueryable<Tag> queryTag = await _tagRepository.GetQueryableAsync();
            IQueryable<UserMember> userMembers = await _userMemberRepository.GetQueryableAsync();
            var queryStoryDetail = from story in queryStory
                                   join categoryinstoryCat in queryStoryInCategory on story.Id equals categoryinstoryCat.StoryGuid
                                   join categoryinstoryStory in queryCategory on categoryinstoryCat.CategoryId equals categoryinstoryStory.Id
                                   join taginstoryTag in queryTagInStory on story.Id equals taginstoryTag.StoryGuid
                                   join TaginstoryStory in queryTag on taginstoryTag.TagId equals TaginstoryStory.Id
                                   join author in userMembers on story.CreatorId equals author.Id
                                   where story.IsShow && !story.IsDeleted
                                   select new { story, categoryinstoryStory, TaginstoryStory, author };
            long countData = await AsyncExecuter.LongCountAsync(queryStoryDetail);
            List<DetailStoryDTO> data = new();
            foreach (var item in queryStoryDetail.ToList())
            {
                DetailStoryDTO story = new()
                {
                    Story_Title = item.story.Story_Title,
                    Story_Synopsis = item.story.Story_Synopsis,
                    Img_Url = item.story.Img_Url,
                    IsShow = item.story.IsShow,
                    NameCategory = item.categoryinstoryStory.NameCategory,
                    TagName = item.TaginstoryStory.TagName,
                    AuthorName = item.author.LastName + " " + item.author.FirstName,
                    TotalFavorite = item.story.TotalFavorite,
                    TotalViews = item.story.TotalView,
                };
                data.Add(story);
            }
            data = data.Skip(requestPage.SkipCount).Take(requestPage.MaxResultCount).ToList();
            return new PagedResultDto<DetailStoryDTO>(countData, data);
        }

        public async Task<PagedResultDto<DetailStoryDTO>> GetListFilterStoryAsync(BaseFilterDto baseFilter)
        {
            IQueryable<Story> queryStory = await Repository.GetQueryableAsync();
            IQueryable<CategoryInStory> queryStoryInCategory = await _categoryinStoryRepository.GetQueryableAsync();
            IQueryable<TagInStory> queryTagInStory = await _tagInstoryRepository.GetQueryableAsync();
            IQueryable<Category> queryCategory = await _categoryRepository.GetQueryableAsync();
            IQueryable<Tag> queryTag = await _tagRepository.GetQueryableAsync();
            IQueryable<UserMember> userMembers = await _userMemberRepository.GetQueryableAsync();
            var queryStoryDetail = from story in queryStory
                                   join categoryinstoryCat in queryStoryInCategory on story.Id equals categoryinstoryCat.StoryGuid
                                   join categoryinstoryStory in queryCategory on categoryinstoryCat.CategoryId equals categoryinstoryStory.Id
                                   join taginstoryTag in queryTagInStory on story.Id equals taginstoryTag.StoryGuid
                                   join TaginstoryStory in queryTag on taginstoryTag.TagId equals TaginstoryStory.Id
                                   join author in userMembers on story.CreatorId equals author.Id
                                   where story.IsShow && !story.IsDeleted
                                   select new { story, categoryinstoryStory, TaginstoryStory, author };
            queryStoryDetail.WhereIf(!string.IsNullOrEmpty(baseFilter.Keyword), x => x.story.Story_Title.Contains(baseFilter.Keyword));
            long countData = await AsyncExecuter.LongCountAsync(queryStoryDetail);
            List<DetailStoryDTO> data = new();
            foreach (var item in queryStoryDetail.ToList())
            {
                DetailStoryDTO story = new()
                {
                    Story_Title = item.story.Story_Title,
                    Story_Synopsis = item.story.Story_Synopsis,
                    Img_Url = item.story.Img_Url,
                    IsShow = item.story.IsShow,
                    NameCategory = item.categoryinstoryStory.NameCategory,
                    TagName = item.TaginstoryStory.TagName,
                    AuthorName = item.author.LastName + " " + item.author.FirstName,
                    TotalFavorite = item.story.TotalFavorite,
                    TotalViews = item.story.TotalView,
                };
                data.Add(story);
            }
            data = data.Skip(baseFilter.SkipCount).Take(baseFilter.MaxResultCount).ToList();
            return new PagedResultDto<DetailStoryDTO>(countData, data);
        }
        public async override Task<DetailStoryDTO> CreateAsync(CreateUpdateStoryDTO input)
        {
            CreateUpdateStoryDTO storyDto = await _storyManage.CreateAsync(input.Story_Title, input.Story_Synopsis, input.Img_Url, input.IsShow, input.TagId, input.CategoryId, input.Rating);
            Story story = ObjectMapper.Map<CreateUpdateStoryDTO, Story>(storyDto);
            IQueryable<CategoryInStory> queryStoryInCategory = await _categoryinStoryRepository.GetQueryableAsync();
            IQueryable<TagInStory> queryTagInStory = await _tagInstoryRepository.GetQueryableAsync();
            await _categoryinStoryRepository.InsertAsync(new CategoryInStory() { CategoryId = storyDto.CategoryId, StoryGuid = storyDto.Id });
            await _tagInstoryRepository.InsertAsync(new TagInStory() { TagId = storyDto.TagId, StoryGuid = storyDto.Id });
            Story result = await Repository.InsertAsync(story);
            int tagId = queryTagInStory.FirstOrDefault(x => x.StoryGuid.Equals(result.Id)).TagId;
            int CatId = queryStoryInCategory.FirstOrDefault(x => x.StoryGuid.Equals(result.Id)).CategoryId;
            Category queryCategory = await _categoryRepository.GetAsync(x => x.Id == CatId);
            Tag queryTag = await _tagRepository.GetAsync(x => x.Id == tagId);
            UserMember author = await _userMemberRepository.GetAsync(x => x.Id.Equals(result.CreatorId));
            DetailStoryDTO storyResult = new()
            {
                Story_Title = storyDto.Story_Title,
                Story_Synopsis = storyDto.Story_Synopsis,
                Img_Url = storyDto.Img_Url,
                IsShow = storyDto.IsShow,
                NameCategory = queryCategory.NameCategory,
                TagName = queryTag.TagName,
                AuthorName = author.LastName + " " + author.FirstName,
                TotalFavorite = result.TotalFavorite,
                TotalViews = result.TotalView,
            };
            return storyResult;
        }
        public override async Task<DetailStoryDTO> UpdateAsync(Guid StoryGuid, CreateUpdateStoryDTO input)
        {
            CreateUpdateStoryDTO updateStory = await _storyManage.UpdateAsync(StoryGuid, input);
            Story story = ObjectMapper.Map<CreateUpdateStoryDTO, Story>(updateStory);

            CategoryInStory categoryInStory = await _categoryinStoryRepository.GetAsync(x => x.CategoryId == updateStory.CategoryId && x.StoryGuid == StoryGuid);
            TagInStory tagInStory = await _tagInstoryRepository.FirstOrDefaultAsync(x => x.TagId == updateStory.TagId && x.StoryGuid == StoryGuid);
            categoryInStory.StoryGuid = updateStory.Id;
            categoryInStory.CategoryId = updateStory.CategoryId;
            tagInStory.StoryGuid = updateStory.Id;
            tagInStory.TagId = updateStory.TagId;
            await _categoryinStoryRepository.UpdateAsync(categoryInStory);
            await _tagInstoryRepository.UpdateAsync(tagInStory);
            await Repository.UpdateAsync(story);
            UserMember author = await _userMemberRepository.GetAsync(x => x.Id.Equals(story.CreatorId));
            IQueryable<CategoryInStory> queryStoryInCategory = await _categoryinStoryRepository.GetQueryableAsync();
            IQueryable<TagInStory> queryTagInStory = await _tagInstoryRepository.GetQueryableAsync();
            int tagId = queryTagInStory.FirstOrDefault(x => x.StoryGuid.Equals(story.Id)).TagId;
            int CatId = queryStoryInCategory.FirstOrDefault(x => x.StoryGuid.Equals(story.Id)).CategoryId;
            Category queryCategory = await _categoryRepository.GetAsync(x => x.Id == CatId);
            Tag queryTag = await _tagRepository.GetAsync(x => x.Id == tagId);
            DetailStoryDTO storyResult = new()
            {
                Story_Title = story.Story_Title,
                Story_Synopsis = story.Story_Synopsis,
                Img_Url = story.Img_Url,
                IsShow = story.IsShow,
                NameCategory = queryCategory.NameCategory,
                TagName = queryTag.TagName,
                AuthorName = author.LastName + " " + author.FirstName,
                TotalFavorite = story.TotalFavorite,
                TotalViews = story.TotalView,
            };
            return storyResult;
        }
    }
}
