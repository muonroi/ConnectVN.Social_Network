using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ConnectVN.Social_Network.Admin.DTO.UserMembers
{
    public class UserMemberDTO : IEntityDto<Guid>
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }
        public Guid Id { get; set; }
    }
}
