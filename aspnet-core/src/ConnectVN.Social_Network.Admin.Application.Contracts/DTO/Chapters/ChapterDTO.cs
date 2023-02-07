using Volo.Abp.Application.Dtos;

namespace ConnectVN.Social_Network.Admin.DTO.Chapters
{
    public class ChapterDTO : IEntityDto<int>
    {
        public string StoryTitle { get; set; }
        public string ChapterTitle { get; set; }
        public string Body { get; set; }
        public string NumberOfChapter { get; set; }
        public int NumberCharacter { get; set; }
        public string Slug { get; set; }
        public int Id { get; set; }
    }
}
