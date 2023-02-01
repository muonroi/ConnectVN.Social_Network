using ConnectVN.Social_Network.Admin.DTO.Chapters;
using ConnectVN.Social_Network.Admin.Infrastructure.Extentions;
using ConnectVN.Social_Network.Chapters;
using ConnectVN.Social_Network.Domain;
using ConnectVN.Social_Network.Storys;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ConnectVN.Social_Network.Manage
{
    public class ChapterManage : DomainService
    {
        private readonly IRepository<Story> _storyRepository;
        private readonly IRepository<Chapter> _chapterRepository;
        public ChapterManage(IRepository<Story> storyRepository, IRepository<Chapter> chapterRepository)
        {
            _storyRepository = storyRepository;
            _chapterRepository = chapterRepository;
        }
        public async Task<CreateUpdateChapter> CreateAsync(
            string chapterTitle,
            string body,
            string numberOfChapter,
            Guid storyGuid
            )
        {
            try
            {
                Story story = await _storyRepository.GetAsync(x => x.Id.Equals(storyGuid));
                if (story == null)
                {
                    throw new UserFriendlyException(ManageMyFunction.GetMessage(EnumStoryErrorCode.ST09.ToString()), Social_NetworkDomainErrorCodes.ErrorWhenGetChapter);
                }
                char[] delimiters = new char[] { ' ', '\r', '\n' };
                int numberCharacter = body.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
                numberOfChapter = $"Chương {numberOfChapter}";
                string slug = ManageMyFunction.ToSlug(numberOfChapter);
                return new CreateUpdateChapter(chapterTitle, body, numberOfChapter, numberCharacter, storyGuid, slug);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ManageMyFunction.GetMessage(ex.ToString()), Social_NetworkDomainErrorCodes.UnknowError);
            }

        }
        public async Task<CreateUpdateChapter> UpdateAsync(
            int id,
            string chapterTitle,
            string body,
            string numberOfChapter,
            Guid storyGuid
            )
        {
            try
            {
                Chapter chapter = await _chapterRepository.GetAsync(x => x.Id == id);
                if (chapter == null)
                {
                    throw new UserFriendlyException(ManageMyFunction.GetMessage(EnumChapterErrorCode.CT11.ToString()), Social_NetworkDomainErrorCodes.ErrorWhenGetChapter);
                }
                Story story = await _storyRepository.GetAsync(x => x.Id.Equals(storyGuid));
                if (story == null)
                {
                    throw new UserFriendlyException(ManageMyFunction.GetMessage(EnumStoryErrorCode.ST09.ToString()), Social_NetworkDomainErrorCodes.ErrorWhenGetChapter);
                }
                char[] delimiters = new char[] { ' ', '\r', '\n' };
                int numberCharacter = body.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
                numberOfChapter = $"Chương {numberOfChapter}";
                string slug = ManageMyFunction.ToSlug(numberOfChapter);
                return new CreateUpdateChapter(chapterTitle, body, numberOfChapter, numberCharacter, storyGuid, slug, id);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ManageMyFunction.GetMessage(ex.ToString()), Social_NetworkDomainErrorCodes.UnknowError);
            }
        }
    }
}
