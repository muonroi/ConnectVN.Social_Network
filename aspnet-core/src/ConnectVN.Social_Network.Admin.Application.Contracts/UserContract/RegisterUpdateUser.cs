using ConnectVN.Social_Network.User;
using System;
using Volo.Abp.Application.Dtos;

namespace ConnectVN.Social_Network.Admin.UserContract
{
    public class RegisterUpdateUser : IEntityDto<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public EnumGender Gender { get; set; }
        public string Avatar { get; set; }
        public Guid Id { get; set; }
    }
}
