using AutoMapper;
using ConnectVN.Social_Network.Admin.DTO;
using ConnectVN.Social_Network.Admin.StoryContract;
using ConnectVN.Social_Network.Storys;

namespace ConnectVN.Social_Network.Admin;

public class Social_NetworkAdminApplicationAutoMapperProfile : Profile
{
    //Đăng kí map
    public Social_NetworkAdminApplicationAutoMapperProfile()
    {
        //Story
        CreateMap<Story, StoryDTO>();
        CreateMap<StoryDTO, Story>();
        CreateMap<CreateUpdateStory, Story>();
    }
}
