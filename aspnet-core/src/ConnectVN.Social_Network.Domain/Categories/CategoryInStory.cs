using ConnectVN.Social_Network.Storys;
using ConnectVN.Social_Network.Tags;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace ConnectVN.Social_Network.Categories
{
    /// <summary>
    /// Table Category of story
    /// </summary>
    public class CategoryInStory : AuditedEntity<int>
    {
        /// <summary>
        /// Guid story
        /// </summary>
        [Required(ErrorMessage = nameof(EnumTagsErrorCode.TT06))]
        public Guid StoryGuid { get; set; }
        public Story Story { get; set; }
        /// <summary>
        /// Id category
        /// </summary>
        [Required(ErrorMessage = nameof(EnumCategoriesErrorCode.CTS01))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
