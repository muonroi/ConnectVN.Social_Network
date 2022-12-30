using ConnectVN.Social_Network.Categories;
using ConnectVN.Social_Network.Chapters;
using ConnectVN.Social_Network.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace ConnectVN.Social_Network.Storys
{
    /// <summary>
    /// Table Story
    /// </summary>
    public class Story : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// Title of story
        /// </summary>
        [Required(ErrorMessage = nameof(EnumStoryErrorCode.ST00))]
        [MaxLength(255, ErrorMessage = nameof(EnumStoryErrorCode.ST01))]
        [MinLength(3, ErrorMessage = nameof(EnumStoryErrorCode.ST02))]
        public string Story_Title { get; set; }
        /// <summary> 
        /// Description of story
        /// </summary>
        [Required(ErrorMessage = nameof(EnumStoryErrorCode.ST03))]
        [MaxLength(5000, ErrorMessage = nameof(EnumStoryErrorCode.ST04))]
        [MinLength(100, ErrorMessage = nameof(EnumStoryErrorCode.ST05))]
        public string Story_Synopsis { get; set; }
        /// <summary>
        /// Url img of story
        /// </summary>
        [Required(ErrorMessage = nameof(EnumStoryErrorCode.ST06))]
        [MaxLength(1000, ErrorMessage = nameof(EnumStoryErrorCode.ST10))]
        public string Img_Url { get; set; }
        /// <summary>
        /// Is show ? tru : false
        /// </summary>
        [Required(ErrorMessage = nameof(EnumStoryErrorCode.ST08))]
        public bool IsShow { get; set; }
        public List<Chapter> Chapters { get; set; }
        public List<CategoryInStory> CategoryInStory { get; set; }
        public List<StoryNotifications> StoryNotifications { get; set; }
        public List<TagInStory> TagInStory { get; set; }



    }
}
