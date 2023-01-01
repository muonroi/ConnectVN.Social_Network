using AutoMapper;
using ConnectVN.Social_Network.Admin.DTO.UserMembers;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Admin;

public class Social_NetworkAdminApplicationAutoMapperProfile : Profile
{
    //Đăng kí map
    public Social_NetworkAdminApplicationAutoMapperProfile()
    {
        //Story 
        CreateMap<IdentityUserDto, UserMemberDTO>();

    }
}
