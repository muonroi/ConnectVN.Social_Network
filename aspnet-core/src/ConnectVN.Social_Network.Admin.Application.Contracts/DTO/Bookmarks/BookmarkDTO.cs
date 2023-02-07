using System;
using Volo.Abp.Application.Dtos;

namespace ConnectVN.Social_Network.Admin.DTO.Bookmarks
{
    public class BookmarkDTO : IEntityDto<int>
    {
        public BookmarkDTO()
        {

        }
        public BookmarkDTO(Guid storyGuid, Guid userGuid, DateTime bookmarkDate, int id = 0)
        {
            StoryGuid = storyGuid;
            UserGuid = userGuid;
            BookmarkDate = bookmarkDate;
            Id = id;
        }
        public Guid StoryGuid { get; set; }
        public Guid UserGuid { get; set; }
        public DateTime BookmarkDate { get; set; }
        public int Id { get; set; }
    }
}
