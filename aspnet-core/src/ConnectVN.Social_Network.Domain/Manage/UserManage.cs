using ConnectVN.Social_Network.Admin.UserContract;
using ConnectVN.Social_Network.Users;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ConnectVN.Social_Network.Manage
{
    public class UserManage : DomainService
    {
        public IRepository<UserMember> _usermemberRepository;
        public UserManage(IRepository<UserMember> usermemberRepository)
        {
            _usermemberRepository = usermemberRepository;
        }
        public UserMember CreateUser(RegisterUpdateUser input)
        {
            return new UserMember(Guid.NewGuid(), input.FirstName, input.LastName, input.UserName, input.Password, input.Email, input.PhoneNumber, input.Address, input.BirthDate, input.Gender, input.Avatar);
        }
    }
}
