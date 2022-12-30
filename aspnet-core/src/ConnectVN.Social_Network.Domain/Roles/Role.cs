using ConnectVN.Social_Network.Tags;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace ConnectVN.Social_Network.Roles
{
    /// <summary>
    /// Table Roles
    /// </summary>
    public class Role : CreationAuditedAggregateRoot<int>
    {
        /// <summary>
        /// User Guid
        /// </summary>
        [Required(ErrorMessage = nameof(EnumRolesErrorCodes.RL00))]
        public Guid UserGuid { get; set; }
        /// <summary>
        /// Group Id
        /// </summary>
        [Required(ErrorMessage = nameof(EnumRolesErrorCodes.RL01))]
        public int GroupId { get; set; }
        public GroupUserMember GroupUserMember { get; set; }

    }
}
