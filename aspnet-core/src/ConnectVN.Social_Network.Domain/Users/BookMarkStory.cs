using ConnectVN.Social_Network.Tags;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace ConnectVN.Social_Network.Users
{
    /// <summary>
    /// Table Bookmark
    /// </summary>
    public class BookMarkStory : AuditedAggregateRoot<int>
    {
        /// <summary>
        /// Story Guid
        /// </summary>
        [Required(ErrorMessage = nameof(EnumTagsErrorCode.TT06))]
        public Guid StoryGuid { get; set; }
        /// <summary>
        /// User Guid
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// BookmarkDate
        /// </summary>
        public DateTime BookmarkDate { get; set; }
        public UserMember UserMember { get; set; }
    }
}
