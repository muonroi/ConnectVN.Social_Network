using ConnectVN.Social_Network.Admin.DTO.Storys;
using ConnectVN.Social_Network.Admin.Infrastructure.Extentions;
using ConnectVN.Social_Network.Admin.Infrastructure.Services;
using ConnectVN.Social_Network.Admin.Setting;
using ConnectVN.Social_Network.Admin.StoryContract;
using ConnectVN.Social_Network.Categories;
using ConnectVN.Social_Network.Domain;
using ConnectVN.Social_Network.Manage;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.Tags;
using ConnectVN.Social_Network.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Security.Encryption;

namespace ConnectVN.Social_Network.Admin.StoryServices
{
    public class StoryService : CrudAppService<Story, DetailStoryDTO, Guid, PagedResultRequestDto, CreateUpdateStoryDTO, CreateUpdateStoryDTO>, IStoryService
    {
        private readonly IStoryServiceAPI _storyServiceAPI;
        private readonly IBlobContainer<StoryFileUpload> _storyFileUpload;
        private readonly StoryManage _storyManage;
        private readonly IRepository<TagInStory> _tagInstoryRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<AppUser> _userMemberRepository;
        public IStringEncryptionService _encryptionService { get; set; }

        protected IStringEncryptionService StringEncryptionService { get; }
        public static readonly List<string> ImageExtensions = new() { "JPG", "JPE", "BMP", "GIF", "PNG", "JPEG" };
        public StoryService(
            IRepository<Story, Guid> repository,
            IRepository<TagInStory> tagInstoryRepository,
            IRepository<Tag> tagRepository,
            IRepository<Category> categoryRepository,
            IRepository<AppUser> userMemberRepository, IStringEncryptionService stringEncryptionService
            , StoryManage storyManage, IBlobContainer<StoryFileUpload> storyFileUpload, IStoryServiceAPI storyServiceAPI, IStringEncryptionService encryptionService) : base(repository)
        {
            _storyManage = storyManage;
            _tagInstoryRepository = tagInstoryRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _userMemberRepository = userMemberRepository;
            StringEncryptionService = stringEncryptionService;
            _storyFileUpload = storyFileUpload;
            _storyServiceAPI = storyServiceAPI;
            _encryptionService = encryptionService;
        }

        public async Task DeleteMultipleStoryAsync(IEnumerable<Guid> guidId)
            // Delete multi story
            => await Repository.DeleteManyAsync(guidId);
        public async override Task<DetailStoryDTO> GetAsync(Guid id)
        {
            try
            {
                String encryptedGoogleAppPassword = _encryptionService.Encrypt(MainSetting.ENV_APP_GOOGLE_PASSWORD);
                Story queryStory = await Repository.GetAsync(id);
                DetailStoryDTO story = null;
                if (queryStory == null)
                {
                    throw new BusinessException(EnumStoryErrorCode.ST10.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                IQueryable<TagInStory> queryTagInStory = await _tagInstoryRepository.GetQueryableAsync();
                if (queryTagInStory == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                int tagId = queryTagInStory.FirstOrDefault(x => x.StoryGuid.Equals(queryStory.Id)) != null ? queryTagInStory.FirstOrDefault(x => x.StoryGuid.Equals(queryStory.Id)).TagId : 0;
                if (tagId == 0)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                Category queryCategory = await _categoryRepository.GetAsync(x => x.Id == queryStory.CategoryId);
                if (queryCategory == null)
                {
                    throw new BusinessException(EnumCategoriesErrorCode.CTS02.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                Tag queryTag = await _tagRepository.GetAsync(x => x.Id == tagId);
                if (queryTag == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                AppUser author = await _userMemberRepository.GetAsync(x => x.Id.Equals(queryStory.CreatorId));
                if (author == null)
                {
                    throw new BusinessException(EnumUserErrorCodes.USR02C.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                story = new()
                {
                    Id = queryStory.Id,
                    Story_Title = queryStory.Story_Title,
                    Story_Synopsis = queryStory.Story_Synopsis,
                    Img_Url = queryStory.Img_Url,
                    IsShow = queryStory.IsShow,
                    NameCategory = queryCategory.NameCategory,
                    TagName = queryTag.TagName,
                    AuthorName = author.Surname + " " + author.Name,
                    TotalFavorite = queryStory.TotalFavorite,
                    TotalViews = queryStory.TotalView,
                    Rating = queryStory.Rating,
                };
                queryStory.TotalView++;
                await Repository.UpdateAsync(queryStory);
                return story;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
        public async Task<PagedResultDto<DetailStoryDTO>> GetListAllStoryAsync(PagedResultRequestDto requestPage)
        {
            try
            {
                PagedResultDto<DetailStoryDTO> pagedResultDto = null;
                IQueryable<Story> queryStory = await Repository.GetQueryableAsync();
                if (queryStory == null)
                {
                    throw new BusinessException(EnumStoryErrorCode.ST10.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                IQueryable<TagInStory> queryTagInStory = await _tagInstoryRepository.GetQueryableAsync();
                if (queryTagInStory == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                IQueryable<Tag> queryTag = await _tagRepository.GetQueryableAsync();
                if (queryTag == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                IQueryable<AppUser> userMembers = await _userMemberRepository.GetQueryableAsync();
                if (userMembers == null)
                {
                    throw new BusinessException(EnumUserErrorCodes.USR02C.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                IQueryable<Category> queryCategory = await _categoryRepository.GetQueryableAsync();
                if (queryCategory == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                var queryStoryDetail = from story in queryStory
                                       join category in queryCategory on story.CategoryId equals category.Id
                                       join tagInStoryGetTag in queryTagInStory on story.Id equals tagInStoryGetTag.StoryGuid
                                       join tagInStoryGetStory in queryTag on tagInStoryGetTag.TagId equals tagInStoryGetStory.Id
                                       join author in userMembers on story.CreatorId equals author.Id
                                       where story.IsShow && !story.IsDeleted
                                       select new { story, category, tagInStoryGetStory, author };
                long countData = await AsyncExecuter.LongCountAsync(queryStoryDetail);
                if (countData == 0)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                List<DetailStoryDTO> data = new();
                foreach (var item in queryStoryDetail.ToList())
                {
                    DetailStoryDTO story = new()
                    {
                        Id = item.story.Id,
                        Story_Title = item.story.Story_Title,
                        Story_Synopsis = item.story.Story_Synopsis,
                        Img_Url = item.story.Img_Url,
                        IsShow = item.story.IsShow,
                        NameCategory = item.category.NameCategory,
                        TagName = item.tagInStoryGetStory.TagName,
                        AuthorName = item.author.Surname + " " + item.author.Name,
                        TotalFavorite = item.story.TotalFavorite,
                        TotalViews = item.story.TotalView,
                        Rating = item.story.Rating,
                    };
                    data.Add(story);
                }
                data = data.Skip(requestPage.SkipCount).Take(requestPage.MaxResultCount).ToList();
                return pagedResultDto = new(countData, data);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }
        }
        public async Task<PagedResultDto<DetailStoryDTO>> GetListFilterStoryAsync(BaseFilterDto baseFilter)
        {
            try
            {
                PagedResultDto<DetailStoryDTO> pagedResultDto = null;
                IQueryable<Story> queryStory = await Repository.GetQueryableAsync();
                if (queryStory == null)
                {
                    throw new BusinessException(EnumStoryErrorCode.ST10.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                IQueryable<TagInStory> queryTagInStory = await _tagInstoryRepository.GetQueryableAsync();
                if (queryTagInStory == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                IQueryable<Tag> queryTag = await _tagRepository.GetQueryableAsync();
                if (queryTag == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                IQueryable<AppUser> userMembers = await _userMemberRepository.GetQueryableAsync();
                if (userMembers == null)
                {
                    throw new BusinessException(EnumUserErrorCodes.USR02C.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                IQueryable<Category> queryCategory = await _categoryRepository.GetQueryableAsync();
                if (queryCategory == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                var queryStoryDetail = from story in queryStory
                                       join cat in queryCategory on story.CategoryId equals cat.Id
                                       join taginstoryTag in queryTagInStory on story.Id equals taginstoryTag.StoryGuid
                                       join TaginstoryStory in queryTag on taginstoryTag.TagId equals TaginstoryStory.Id
                                       join author in userMembers on story.CreatorId equals author.Id
                                       where story.IsShow && !story.IsDeleted
                                       select new { story, cat, TaginstoryStory, author };
                queryStoryDetail.WhereIf(!string.IsNullOrEmpty(baseFilter.Keyword), x => x.story.Story_Title.Contains(baseFilter.Keyword));
                long countData = await AsyncExecuter.LongCountAsync(queryStoryDetail);
                if (countData == 0)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetStory);
                }
                List<DetailStoryDTO> data = new();
                foreach (var item in queryStoryDetail.ToList())
                {
                    DetailStoryDTO story = new()
                    {
                        Id = item.story.Id,
                        Story_Title = item.story.Story_Title,
                        Story_Synopsis = item.story.Story_Synopsis,
                        Img_Url = item.story.Img_Url,
                        IsShow = item.story.IsShow,
                        NameCategory = item.cat.NameCategory,
                        TagName = item.TaginstoryStory.TagName,
                        AuthorName = item.author.Surname + " " + item.author.Name,
                        TotalFavorite = item.story.TotalFavorite,
                        TotalViews = item.story.TotalView,
                        Rating = item.story.Rating,
                    };
                    data.Add(story);
                }
                data = data.Skip(baseFilter.SkipCount).Take(baseFilter.MaxResultCount).ToList();
                return pagedResultDto = new(countData, data);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
        private static bool CheckTypeFileIsImage(IFormFile file)
        {
            try
            {
                if (ImageExtensions.Any(x => x.Equals(file.ContentType.Split('/')[1].ToUpperInvariant())))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
        public async override Task<DetailStoryDTO> CreateAsync([FromForm] CreateUpdateStoryDTO input)
        {
            try
            {
                string url_img = "";
                if (CheckTypeFileIsImage(input.FormFile) && input.FormFile.Length > 0)
                {
                    Stream streamImg = input.FormFile.OpenReadStream();
                    IApiResponse<string> ressultUpload = _storyServiceAPI.UploadFileImg(new StreamPart(streamImg, input.FormFile.FileName, input.FormFile.ContentType)).GetAwaiter().GetResult();
                    url_img = ressultUpload.Content ?? "";
                }
                CreateUpdateStoryDTO storyDto = await _storyManage.CreateAsync(input.Story_Title, input.Story_Synopsis, input.IsShow, input.NewTagId, input.NewCategoryId, input.Rating);

                Story story = ObjectMapper.Map<CreateUpdateStoryDTO, Story>(storyDto);
                story.Img_Url = url_img;
                story.CategoryId = storyDto.NewCategoryId;

                IQueryable<TagInStory> queryTagInStory = await _tagInstoryRepository.GetQueryableAsync();
                if (queryTagInStory == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenCreateStory);
                }

                Story result = await Repository.InsertAsync(story);
                if (result == null)
                {
                    throw new BusinessException(EnumStoryErrorCode.ST11.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenCreateStory);
                }
                TagInStory insertTaginStory = await _tagInstoryRepository.InsertAsync(new TagInStory() { TagId = storyDto.NewTagId, StoryGuid = result.Id }).ConfigureAwait(false);
                if (insertTaginStory == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT09.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenCreateStory);
                }

                Category queryCategory = await _categoryRepository.GetAsync(x => x.Id == storyDto.NewCategoryId);
                if (queryCategory == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenCreateStory);
                }
                Tag queryTag = await _tagRepository.GetAsync(x => x.Id == storyDto.NewTagId);
                if (queryTag == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenCreateStory);
                }
                AppUser author = await _userMemberRepository.GetAsync(x => x.Id.Equals(result.CreatorId));
                if (author == null)
                {
                    throw new BusinessException(EnumUserErrorCodes.USR02C.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenCreateStory);
                }
                DetailStoryDTO storyResult = new()
                {
                    Id = story.Id,
                    Story_Title = storyDto.Story_Title,
                    Story_Synopsis = storyDto.Story_Synopsis,
                    Img_Url = url_img,
                    IsShow = storyDto.IsShow,
                    NameCategory = queryCategory.NameCategory,
                    TagName = queryTag.TagName,
                    AuthorName = author.Surname + " " + author.Name,
                    TotalFavorite = result.TotalFavorite,
                    TotalViews = result.TotalView,
                    Rating = result.Rating,
                };
                return storyResult;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }
        }
        public async override Task<DetailStoryDTO> UpdateAsync(Guid StoryGuid, [FromForm] CreateUpdateStoryDTO input)
        {
            try
            {
                DetailStoryDTO storyResult = null;
                string url_img = "";

                CreateUpdateStoryDTO updateStory = await _storyManage.UpdateAsync(StoryGuid, input);

                Story story = await Repository.GetAsync(x => x.Id.Equals(StoryGuid));
                if (story == null)
                {
                    throw new BusinessException(EnumStoryErrorCode.ST11.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenUpdateStory);
                }
                if (!story.Img_Url.IsNullOrEmpty())
                {
                    _storyServiceAPI.DeleteFileImg(StoryGuid).GetAwaiter().GetResult();
                }
                if (CheckTypeFileIsImage(input.FormFile) && input.FormFile.Length > 0)
                {
                    Stream streamImg = input.FormFile.OpenReadStream();
                    IApiResponse<string> ressultUpload = _storyServiceAPI.UploadFileImg(new StreamPart(streamImg, input.FormFile.FileName, input.FormFile.ContentType)).GetAwaiter().GetResult();
                    url_img = ressultUpload.Content ?? "";
                }
                story.Story_Title = updateStory.Story_Title;
                story.Story_Synopsis = updateStory.Story_Synopsis;
                story.Rating = updateStory.Rating;
                story.IsShow = updateStory.IsShow;
                story.Img_Url = url_img == "" ? story.Img_Url : url_img;
                story.CategoryId = input.NewCategoryId;

                TagInStory tagInStory = await _tagInstoryRepository.FirstOrDefaultAsync(x => x.TagId == input.OldTagId && x.StoryGuid == StoryGuid);
                if (tagInStory == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenCreateStory);
                }
                await _tagInstoryRepository.DeleteAsync(tagInStory);
                await _tagInstoryRepository.InsertAsync(new TagInStory { TagId = input.NewTagId, StoryGuid = StoryGuid });

                Guid authorGuid = Repository.GetAsync(x => x.Id == StoryGuid).Result.CreatorId.Value;
                AppUser author = await _userMemberRepository.GetAsync(x => x.Id.Equals(authorGuid));
                if (author == null)
                {
                    throw new BusinessException(EnumUserErrorCodes.USR02C.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenUpdateStory);
                }
                Tag queryTag = await _tagRepository.GetAsync(x => x.Id == updateStory.NewTagId);
                if (queryTag == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenUpdateStory);
                }
                Category queryCategory = await _categoryRepository.GetAsync(x => x.Id == input.NewCategoryId);
                if (queryCategory == null)
                {
                    throw new BusinessException(EnumTagsErrorCode.TT08.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenUpdateStory);
                }
                await Repository.UpdateAsync(story);

                storyResult = new()
                {
                    Id = story.Id,
                    Story_Title = story.Story_Title,
                    Story_Synopsis = story.Story_Synopsis,
                    Img_Url = url_img,
                    IsShow = story.IsShow,
                    NameCategory = queryCategory.NameCategory,
                    TagName = queryTag.TagName,
                    AuthorName = author.Surname + " " + author.Name,
                    TotalFavorite = story.TotalFavorite,
                    TotalViews = story.TotalView,
                    ConcurrencyStamp = story.ConcurrencyStamp,
                    Rating = story.Rating,
                };

                return storyResult;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }
        }
        public async Task<string> UploadImg([FromForm] IFormFile files)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await files.CopyToAsync(memoryStream).ConfigureAwait(false);
                string id = Guid.NewGuid().ToString() + "." + files.ContentType;
                string filename = FolderSetting.IMG_STORY + $"{ConstName.DefaultNameImg + "_" + files.FileName}";
                await _storyFileUpload.SaveAsync(filename, memoryStream.ToArray(), overrideExisting: true).ConfigureAwait(false);
                string result = id + '*' + filename;
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
        public async Task<bool> DeleteImg(Guid guidStory)
        {
            try
            {
                Story currentFile = await Repository.GetAsync(x => x.Id == guidStory);
                string contentType = currentFile.Img_Url.Split('.', '*')[1];
                if (currentFile != null)
                {
                    string fileName = currentFile.Img_Url.Split('*').LastOrDefault().ToString();
                    return await _storyFileUpload.DeleteAsync(fileName);
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
        public async Task<FileResult> GetImg(Guid guidStory)
        {
            try
            {
                Story currentFile = await Repository.GetAsync(x => x.Id == guidStory);
                string contentType = currentFile.Img_Url.Split('.', '*')[1];
                if (currentFile != null)
                {
                    string fileName = currentFile.Img_Url.Split('*').LastOrDefault().ToString();
                    byte[] myfile = await _storyFileUpload.GetAllBytesOrNullAsync(fileName);
                    return new FileContentResult(myfile, contentType);
                }

                throw new FileNotFoundException();
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.ToString().GetMessage(), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
    }
}
