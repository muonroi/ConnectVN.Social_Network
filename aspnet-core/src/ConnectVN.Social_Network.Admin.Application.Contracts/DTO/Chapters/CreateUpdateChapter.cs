using System;
using Volo.Abp.Application.Dtos;
namespace ConnectVN.Social_Network.Admin.DTO.Chapters
{
    public class CreateUpdateChapter : IEntityDto<int>
    {
        public CreateUpdateChapter()
        { }
        public CreateUpdateChapter(string chapterTitle, string body, string numberOfChapter, int numberCharacter, Guid storyGuid, string slug, int id = 0)
        {
            Id = id;
            ChapterTitle = chapterTitle;
            Body = body;
            NumberOfChapter = numberOfChapter;
            NumberCharacter = numberCharacter;
            StoryGuid = storyGuid;
            Slug = slug;
        }
        public int Id { get; set; }
        public string ChapterTitle { get; set; }
        public string Body { get; set; }
        public string NumberOfChapter { get; set; }
        public int NumberCharacter { get; set; }
        public string Slug { get; set; }
        public Guid StoryGuid { get; set; }
    }
}
