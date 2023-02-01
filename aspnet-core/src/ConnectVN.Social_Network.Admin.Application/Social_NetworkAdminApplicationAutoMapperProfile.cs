using AutoMapper;
using ConnectVN.Social_Network.Admin.DTO.Chapters;
using ConnectVN.Social_Network.Admin.DTO.Storys;
using ConnectVN.Social_Network.Admin.DTO.UserMembers;
using ConnectVN.Social_Network.Chapters;
using ConnectVN.Social_Network.Storys;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Admin;

public class Social_NetworkAdminApplicationAutoMapperProfile : Profile
{
    //Đăng kí map
    public Social_NetworkAdminApplicationAutoMapperProfile()
    {
        //Story 
        CreateMap<IdentityUserDto, UserMemberDTO>();
        CreateMap<CreateUpdateStoryDTO, Story>();
        CreateMap<CreateUpdateChapter, Chapter>();
        CreateMap<Chapter, ChapterDTO>();

    }
}
