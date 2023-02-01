using ConnectVN.Social_Network.Admin.ChapterContract;
using ConnectVN.Social_Network.Admin.DTO.Chapters;
using ConnectVN.Social_Network.Chapters;
using ConnectVN.Social_Network.Domain;
using ConnectVN.Social_Network.Manage;
using ConnectVN.Social_Network.Storys;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ConnectVN.Social_Network.Admin.Infrastructure.Extentions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ConnectVN.Social_Network.Admin.ChapterServices
{
    [Produces(MediaTypeNames.Application.Json,
  MediaTypeNames.Application.Xml)]
    public class ChapterService : CrudAppService<Chapter, ChapterDTO, int, PagedResultRequestDto, CreateUpdateChapter, CreateUpdateChapter>, IChapterService
    {
        private readonly ChapterManage _chapterManage;
        private readonly IRepository<Story> _storyRepository;
        public ChapterService(IRepository<Chapter, int> repository, IRepository<Story> storyRepository, ChapterManage chapterManage) : base(repository)
        {
            _storyRepository = storyRepository;
            _chapterManage = chapterManage;
        }
        public async Task DeleteMultipleChapter(IEnumerable<int> ids)
       => await Repository.DeleteManyAsync(ids);
        /// <summary>
        /// Create new chapter of story by guid story
        /// </summary>
        /// <param name="input">request create chapter</param>
        /// <returns></returns>
        public async override Task<ChapterDTO> CreateAsync([FromForm] CreateUpdateChapter input)
        {
            CreateUpdateChapter result = await _chapterManage.CreateAsync(input.ChapterTitle, input.Body, input.NumberOfChapter, input.StoryGuid);
            Story story = await _storyRepository.GetAsync(x => x.Id.Equals(input.StoryGuid));
            Chapter chapter = ObjectMapper.Map<CreateUpdateChapter, Chapter>(result);
            Chapter newChapter = await Repository.InsertAsync(chapter);
            ChapterDTO chapterDTO = ObjectMapper.Map<Chapter, ChapterDTO>(newChapter);
            chapterDTO.StoryTitle = story.Story_Title;
            return chapterDTO;
        }
        /// <summary>
        /// Get chapter of story by id chapter
        /// </summary>
        /// <param name="id">Id of chapter</param>
        /// <returns></returns>
        public async override Task<ChapterDTO> GetAsync(int id)
        {
            Chapter newChapters = await Repository.GetAsync(id);
            Story result = await _storyRepository.GetAsync(x => x.Id.Equals(newChapters.StoryGuid));
            ChapterDTO temp = new()
            {
                ChapterTitle = newChapters.ChapterTitle,
                Body = newChapters.Body,
                NumberOfChapter = newChapters.NumberOfChapter,
                NumberCharacter = newChapters.NumberCharacter,
                Slug = newChapters.Slug,
                StoryTitle = result.Story_Title
            };
            return temp;
        }
        /// <summary>
        /// Get list chapter (paging)
        /// </summary>
        /// <param name="input">inclue --> skip and max row <--</param>
        /// <returns></returns>
        /// <exception cref="BusinessException">Exeption when get error</exception>
        public async override Task<PagedResultDto<ChapterDTO>> GetListAsync(PagedResultRequestDto input)
        {
            PagedResultDto<ChapterDTO> pagedResultDto = null;
            IQueryable<Chapter> newChapters = await Repository.GetQueryableAsync();
            IQueryable<Story> stories = await _storyRepository.GetQueryableAsync();
            var result = from chap in newChapters
                         join story in stories on chap.StoryGuid equals story.Id
                         select new { chap, story };
            if (result == null)
            {
                throw new BusinessException(EnumChapterErrorCode.CT11.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenGetChapter);
            }
            List<ChapterDTO> chapterDTO = new();
            foreach (var item in result)
            {
                ChapterDTO temp = new()
                {
                    ChapterTitle = item.chap.ChapterTitle,
                    Body = item.chap.Body,
                    NumberOfChapter = item.chap.NumberOfChapter,
                    NumberCharacter = item.chap.NumberCharacter,
                    Slug = item.chap.Slug,
                    Id = item.chap.Id,
                    StoryTitle = item.story.Story_Title
                };
                chapterDTO.Add(temp);
            }
            int longCountItem = chapterDTO.Count;
            return pagedResultDto = new PagedResultDto<ChapterDTO>(longCountItem, chapterDTO);
        }

        /// <summary>
        /// Update chapter by id chapter
        /// </summary>
        /// <param name="id">id chapter</param>
        /// <param name="input">request new info update</param>
        /// <returns></returns>
        public async override Task<ChapterDTO> UpdateAsync(int id, [FromForm] CreateUpdateChapter input)
        {
            Chapter currentDb = await Repository.GetAsync(id);
            Story resultStory = await _storyRepository.GetAsync(x => x.Id.Equals(currentDb.StoryGuid));
            CreateUpdateChapter result = await _chapterManage.UpdateAsync(id, input.ChapterTitle, input.Body, input.NumberOfChapter, input.StoryGuid);
            currentDb.ChapterTitle = result.ChapterTitle;
            currentDb.Body = result.Body;
            currentDb.NumberCharacter = result.NumberCharacter;
            currentDb.NumberOfChapter = result.NumberOfChapter;
            currentDb.Slug = result.Slug;
            await Repository.UpdateAsync(currentDb);
            ChapterDTO chapterDTO = ObjectMapper.Map<Chapter, ChapterDTO>(currentDb);
            chapterDTO.StoryTitle = resultStory.Story_Title;
            return chapterDTO;
        }
    }
}
