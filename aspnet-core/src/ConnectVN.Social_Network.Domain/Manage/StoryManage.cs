using ConnectVN.Social_Network.Admin.DTO.Storys;
using ConnectVN.Social_Network.Admin.Infrastructure.Extentions;
using ConnectVN.Social_Network.Categories;
using ConnectVN.Social_Network.Domain;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.Tags;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ConnectVN.Social_Network.Manage
{
    public class StoryManage : DomainService
    {
        private readonly IRepository<Story> _storyRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Category> _categoryRepository;

        public StoryManage(IRepository<Story> storyRepository, IRepository<Tag> tagRepository, IRepository<Category> categoryRepository)
        {
            _storyRepository = storyRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }
        public async Task<CreateUpdateStoryDTO> CreateAsync(
                        string story_Title,
                        string story_Synopsi,
                        string img_Url,
                        bool isShow,
                        int idTag,
                        int idCat,
                        double Rating = 0)
        {
            if (await _storyRepository.AnyAsync(x => x.Story_Title.Equals(story_Title)))
            {
                throw new UserFriendlyException(GetErrorMessage.GetMessage(EnumStoryErrorCode.ST09.ToString()), Social_NetworkDomainErrorCodes.ExistsStoryTitle);
            }
            Tag tag = await _tagRepository.GetAsync(x => x.Id == idTag);
            if (tag == null)
            {
                throw new UserFriendlyException(GetErrorMessage.GetMessage(EnumTagsErrorCode.TT08.ToString()), Social_NetworkDomainErrorCodes.NoExistsTag);
            }
            Category category = await _categoryRepository.GetAsync(x => x.Id == idCat);

            if (category == null)
            {
                throw new UserFriendlyException(GetErrorMessage.GetMessage(EnumCategoriesErrorCode.CTS02.ToString()), Social_NetworkDomainErrorCodes.NotExistsCategory);
            }
            return new CreateUpdateStoryDTO(Guid.NewGuid(), tag.Id, category.Id, story_Title, story_Synopsi, img_Url, isShow, Rating);
        }
        public async Task<CreateUpdateStoryDTO> UpdateAsync(Guid id, CreateUpdateStoryDTO input)
        {
            Story story = await _storyRepository.GetAsync(x => x.Id == id);
            if (story == null)
            {
                throw new UserFriendlyException(GetErrorMessage.GetMessage(EnumStoryErrorCode.ST09.ToString()), Social_NetworkDomainErrorCodes.NotExistsStory);
            }
            Tag tag = await _tagRepository.GetAsync(x => x.Id == input.TagId);
            if (tag == null)
            {
                throw new UserFriendlyException(GetErrorMessage.GetMessage(EnumTagsErrorCode.TT08.ToString()), Social_NetworkDomainErrorCodes.NoExistsTag);
            }
            Category category = await _categoryRepository.GetAsync(x => x.Id == input.CategoryId);

            if (category == null)
            {
                throw new UserFriendlyException(GetErrorMessage.GetMessage(EnumCategoriesErrorCode.CTS02.ToString()), Social_NetworkDomainErrorCodes.NotExistsCategory);
            }
            return new CreateUpdateStoryDTO(Guid.NewGuid(), tag.Id, category.Id, input.Story_Title, input.Story_Synopsis, input.Img_Url, input.IsShow, input.Rating);
        }
    }
}
