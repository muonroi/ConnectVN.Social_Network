using ConnectVN.Social_Network.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace ConnectVN.Social_Network.Users
{
    /// <summary>
    /// Table follow
    /// </summary>
    public class FollowingAuthor : CreationAuditedEntity<int>
    {
        /// <summary>
        /// UserGuid
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// StoryGuid
        /// </summary>
        [Required(ErrorMessage = nameof(EnumTagsErrorCode.TT05))]
        public Guid StoryGuid { get; set; }
        public AppUser UserMember { get; set; }
    }
}
