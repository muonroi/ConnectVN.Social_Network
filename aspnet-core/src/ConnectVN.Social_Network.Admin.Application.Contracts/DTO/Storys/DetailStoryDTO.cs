using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ConnectVN.Social_Network.Admin.DTO.Storys
{
    public class DetailStoryDTO : IEntityDto<Guid>
    {
        public string Story_Title { get; set; }
        public string Story_Synopsis { get; set; }
        public string Img_Url { get; set; }
        public bool IsShow { get; set; }
        public string TagName { get; set; }
        public string NameCategory { get; set; }
        public double Rating { get; set; }
        public string AuthorName { get; set; }
        public string Slug { get; set; }
        public int TotalViews { get; set; }
        public int TotalFavorite { get; set; }
        public Guid Id { get; set; }
    }
}
