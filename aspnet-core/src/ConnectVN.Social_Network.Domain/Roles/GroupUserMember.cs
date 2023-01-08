using ConnectVN.Social_Network.Users;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace ConnectVN.Social_Network.Roles
{
    /// <summary>
    /// Table of group user
    /// </summary>
    public class GroupUserMember : AuditedAggregateRoot<int>
    {
        /// <summary>
        /// Admin
        /// </summary>
        public EnumManage Manage { get; set; }
        /// <summary>
        /// Staff
        /// </summary>
        public EnumStaff Staff { get; set; }
        /// <summary>
        /// Viewer (Have owned account)
        /// </summary>
        public EnumViewer Viewer { get; set; }
        /// <summary>
        /// Guest (haven't account)
        /// </summary>
        public EnumGuest Guest { get; set; }
        public List<AppUser> UserMembers { get; set; }
        public List<Role> Roles { get; set; }

    }
}
