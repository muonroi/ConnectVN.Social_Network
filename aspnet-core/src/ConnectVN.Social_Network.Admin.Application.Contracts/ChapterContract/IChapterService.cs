using ConnectVN.Social_Network.Admin.DTO.Chapters;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ConnectVN.Social_Network.Admin.ChapterContract
{
    public interface IChapterService : ICrudAppService<ChapterDTO, int, PagedResultRequestDto, CreateUpdateChapter>
    {
        public Task DeleteMultipleChapter(IEnumerable<int> ids);
    }
}
