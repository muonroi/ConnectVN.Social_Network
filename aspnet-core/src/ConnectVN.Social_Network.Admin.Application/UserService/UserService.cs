using ConnectVN.Social_Network.Admin.DTO;
using ConnectVN.Social_Network.Admin.DTO.UserMembers;
using ConnectVN.Social_Network.Admin.Infrastructure.Extentions;
using ConnectVN.Social_Network.Admin.Infrastructure.Services;
using ConnectVN.Social_Network.Admin.UserContract;
using ConnectVN.Social_Network.Domain;
using ConnectVN.Social_Network.Manage;
using ConnectVN.Social_Network.Users;
using Refit;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace ConnectVN.Social_Network.Admin.UserService
{
    public class UserServices : CrudAppService<IdentityUser, UserMemberDTO, Guid, PagedResultRequestDto, RegisterUpdateUser, RegisterUpdateUser>, IUserService
    {
        private readonly UserManage _usermanage;
        private readonly IRepository<UserMember> _userMemberRepository;
        private readonly IUserServiceAPI _userServiceApi;
        public UserServices(IRepository<IdentityUser, Guid> repository, IRepository<UserMember> userMemberRepository, IUserServiceAPI userServiceApi, UserManage usermanage) : base(repository)
        {
            _userMemberRepository = userMemberRepository;
            _userServiceApi = userServiceApi;
            _usermanage = usermanage;

        }
        public async override Task<UserMemberDTO> CreateAsync(RegisterUpdateUser input)
        {
            UserMember userMember = _usermanage.CreateUser(input);
            UserSigupDTO sigupDTO = new()
            {
                UserName = input.UserName,
                EmailAddress = input.Email,
                Password = input.Password,
                AppName = input.UserName
            };
            IApiResponse<IdentityUserDto> resultUser = _userServiceApi.RegisterAsync(sigupDTO).GetAwaiter().GetResult();
            if (!resultUser.IsSuccessStatusCode)
            {
                throw new BusinessException(EnumUserErrorCodes.USR29C.ToString().GetMessage(), Social_NetworkDomainErrorCodes.ErrorWhenRegister);
            }
            await _userMemberRepository.InsertAsync(userMember);
            resultUser.Content.Surname = input.LastName;
            resultUser.Content.Name = input.FirstName;
            resultUser.Content.PhoneNumber = input.PhoneNumber;
            return ObjectMapper.Map<IdentityUserDto, UserMemberDTO>(resultUser.Content);
        }
    }
}
