using ConnectVN.Social_Network.Admin.DTO;
using ConnectVN.Social_Network.Admin.StoryContract;
using ConnectVN.Social_Network.Categories;
using ConnectVN.Social_Network.Manage;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ConnectVN.Social_Network.Admin.StoryServices
{
    public class StoryService : CrudAppService<Story, StoryDTO, Guid, PagedResultRequestDto, StoryDTO, StoryDTO>, IStoryService
    {
        private readonly StoryManage _storyManage;
        private readonly IRepository<TagInStory> _tagInstoryRepository;
        private readonly IRepository<CategoryInStory> _categoryinStoryRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Category> _categoryRepository;
        public StoryService(
            IRepository<Story, Guid> repository,
            IRepository<TagInStory> tagInstoryRepository,
            IRepository<CategoryInStory> categoryinStoryRepository,
            IRepository<Tag> tagRepository,
            IRepository<Category> categoryRepository
            , StoryManage storyManage) : base(repository)
        {
            _storyManage = storyManage;
            _categoryinStoryRepository = categoryinStoryRepository;
            _tagInstoryRepository = tagInstoryRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        public async Task DeleteMultipleStoryAsync(IEnumerable<Guid> guidId)
        {
            await Repository.DeleteManyAsync(guidId);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<PagedResultDto<StoryDTO>> GetListAllStoryAsync()
        {
            IQueryable<Story> queryStory = await Repository.GetQueryableAsync();
            IQueryable<CategoryInStory> queryStoryInCategory = await _categoryinStoryRepository.GetQueryableAsync();
            IQueryable<TagInStory> queryTagInStory = await _tagInstoryRepository.GetQueryableAsync();
            IQueryable<Category> queryCategory = await _categoryRepository.GetQueryableAsync();
            IQueryable<Tag> queryTag = await _tagRepository.GetQueryableAsync();
            var queryStoryDetail = from story in queryStory
                                   join categoryinstoryCat in queryStoryInCategory on story.Id equals categoryinstoryCat.StoryGuid
                                   join categoryinstoryStory in queryCategory on categoryinstoryCat.CategoryId equals categoryinstoryStory.Id
                                   join taginstoryTag in queryTagInStory on story.Id equals taginstoryTag.StoryGuid
                                   join TaginstoryStory in queryTag on taginstoryTag.TagId equals TaginstoryStory.Id
                                   where story.IsShow && !story.IsDeleted
                                   select new { story, categoryinstoryStory, TaginstoryStory };
            long countData = await AsyncExecuter.LongCountAsync(queryStoryDetail);

            List<StoryDTO> data = new();
            foreach (var item in queryStoryDetail.ToList())
            {
                StoryDTO story = new()
                {
                    Story_Title = item.story.Story_Title,
                    Story_Synopsis = item.story.Story_Synopsis,
                    Img_Url = item.story.Img_Url,
                    IsShow = item.story.IsShow,
                    NameCategory = item.categoryinstoryStory.NameCategory,
                    TagName = item.TaginstoryStory.TagName
                };
                data.Add(story);
            }
            return new PagedResultDto<StoryDTO>(countData, data);
        }

        public async Task<PagedResultDto<StoryDTO>> GetListFilterStoryAsync(BaseFilterDto baseFilter)
        {
            IQueryable<Story> query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(baseFilter.Keyword), x => x.Story_Title.Contains(baseFilter.Keyword));
            long countData = await AsyncExecuter.LongCountAsync(query);
            List<Story> data = await AsyncExecuter.ToListAsync(query.Skip(baseFilter.SkipCount).Take(baseFilter.MaxResultCount));
            return new PagedResultDto<StoryDTO>(countData, ObjectMapper.Map<List<Story>, List<StoryDTO>>(data));
        }
        public async override Task<StoryDTO> CreateAsync(StoryDTO input)
        {
            StoryDTO storyDto = await _storyManage.CreateAsync(input.Story_Title, input.Story_Synopsis, input.Img_Url, input.IsShow, input.TagName, input.NameCategory, input.Rating);
            Story story = ObjectMapper.Map<StoryDTO, Story>(storyDto);
            await _categoryinStoryRepository.InsertAsync(new CategoryInStory() { CategoryId = storyDto.CategoryId, StoryGuid = storyDto.Id });
            await _tagInstoryRepository.InsertAsync(new TagInStory() { TagId = storyDto.TagId, StoryGuid = storyDto.Id });
            Story result = await Repository.InsertAsync(story);
            return ObjectMapper.Map<Story, StoryDTO>(result);
        }
        public override async Task<StoryDTO> UpdateAsync(Guid StoryGuid, StoryDTO input)
        {
            StoryDTO updateStory = await _storyManage.UpdateAsync(StoryGuid, input);
            Story story = ObjectMapper.Map<StoryDTO, Story>(updateStory);

            CategoryInStory categoryInStory = await _categoryinStoryRepository.GetAsync(x => x.CategoryId == updateStory.CategoryId && x.StoryGuid == StoryGuid);
            TagInStory tagInStory = await _tagInstoryRepository.FirstOrDefaultAsync(x => x.TagId == updateStory.TagId && x.StoryGuid == StoryGuid);
            categoryInStory.StoryGuid = updateStory.Id;
            categoryInStory.CategoryId = updateStory.CategoryId;
            tagInStory.StoryGuid = updateStory.Id;
            tagInStory.TagId = updateStory.TagId;
            await _categoryinStoryRepository.UpdateAsync(categoryInStory);
            await _tagInstoryRepository.UpdateAsync(tagInStory);
            await Repository.UpdateAsync(story);
            return ObjectMapper.Map<Story, StoryDTO>(story);
        }
    }
}
