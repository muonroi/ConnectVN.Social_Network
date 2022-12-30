using ConnectVN.Social_Network.Tags;
using ConnectVN.Social_Network.Users;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace ConnectVN.Social_Network.Storys
{
    /// <summary>
    /// Table StoryPublished
    /// </summary>
    public class StoryPublish : AuditedAggregateRoot<int>
    {
        /// <summary>
        /// Guid story
        /// </summary>
        [Required(ErrorMessage = nameof(EnumTagsErrorCode.TT06))]
        public Guid StoryGuid { get; set; }
        /// <summary>
        /// Guid User
        /// </summary>
        public Guid UserGuid { get; set; }
        public UserMember UserMember { get; set; }

    }
}
