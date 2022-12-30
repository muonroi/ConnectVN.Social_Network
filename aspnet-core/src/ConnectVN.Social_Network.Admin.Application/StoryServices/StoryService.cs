using ConnectVN.Social_Network.Admin.DTO;
using ConnectVN.Social_Network.Admin.StoryContract;
using ConnectVN.Social_Network.Storys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ConnectVN.Social_Network.Admin.StoryServices
{
    public class StoryService : CrudAppService<Story, StoryDTO, Guid, PagedResultRequestDto, CreateUpdateStory, CreateUpdateStory>, IStoryService
    {
        public StoryService(IRepository<Story, Guid> repository) : base(repository)
        {
        }

        public async Task DeleteMultipleStoryAsync(IEnumerable<Guid> guidId)
        {
            await Repository.DeleteManyAsync(guidId);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<PagedResultDto<StoryDTO>> GetListAllStoryAsync()
        {
            IQueryable<Story> query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsShow);
            long countData = await AsyncExecuter.LongCountAsync(query);
            List<Story> data = await AsyncExecuter.ToListAsync(query);
            return new PagedResultDto<StoryDTO>(countData, ObjectMapper.Map<List<Story>, List<StoryDTO>>(data));
        }

        public async Task<PagedResultDto<StoryDTO>> GetListFilterStoryAsync(BaseFilterDto baseFilter)
        {
            IQueryable<Story> query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(baseFilter.Keyword), x => x.Story_Title.Contains(baseFilter.Keyword));
            long countData = await AsyncExecuter.LongCountAsync(query);
            List<Story> data = await AsyncExecuter.ToListAsync(query.Skip(baseFilter.SkipCount).Take(baseFilter.MaxResultCount));
            return new PagedResultDto<StoryDTO>(countData, ObjectMapper.Map<List<Story>, List<StoryDTO>>(data));
        }
    }
}
