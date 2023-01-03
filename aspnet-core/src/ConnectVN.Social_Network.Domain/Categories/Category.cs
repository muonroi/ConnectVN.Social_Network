using ConnectVN.Social_Network.Storys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace ConnectVN.Social_Network.Categories
{
    /// <summary>
    /// Category Table
    /// </summary>
    public class Category : CreationAuditedAggregateRoot<int>
    {
        /// <summary>
        /// Name of category
        /// </summary>
        [Required(ErrorMessage = nameof(EnumCategoriesErrorCode.CTS01))]
        public string NameCategory { get; set; }
        /// <summary>
        /// Status category
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Storys of category
        /// </summary>
        public List<Story> Storys { get; set; }
    }
}
