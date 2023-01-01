using ConnectVN.Social_Network.Admin.DTO.UserMembers;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Admin.UserContract
{
    public interface IUserService : ICrudAppService<UserMemberDTO, Guid, PagedResultRequestDto,
        RegisterUpdateUser>
    {
    }
}
