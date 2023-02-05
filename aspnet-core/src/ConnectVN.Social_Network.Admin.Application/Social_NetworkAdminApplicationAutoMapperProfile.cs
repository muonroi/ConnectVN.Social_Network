using AutoMapper;
using ConnectVN.Social_Network.Admin.DTO.Bookmarks;
using ConnectVN.Social_Network.Admin.DTO.Chapters;
using ConnectVN.Social_Network.Admin.DTO.Storys;
using ConnectVN.Social_Network.Admin.DTO.UserMembers;
using ConnectVN.Social_Network.Chapters;
using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.Users;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Admin;

public class Social_NetworkAdminApplicationAutoMapperProfile : Profile
{
    //Đăng kí map
    public Social_NetworkAdminApplicationAutoMapperProfile()
    {
        //User
        CreateMap<IdentityUserDto, UserMemberDTO>();
        //Story
        CreateMap<CreateUpdateStoryDTO, Story>();
        //Chapter
        CreateMap<CreateUpdateChapter, Chapter>();
        CreateMap<Chapter, ChapterDTO>();
        //Bookmark
        CreateMap<BookMarkStory, BookmarkDTO>();
        CreateMap<BookmarkDTO, BookMarkStory>();
    }
}
