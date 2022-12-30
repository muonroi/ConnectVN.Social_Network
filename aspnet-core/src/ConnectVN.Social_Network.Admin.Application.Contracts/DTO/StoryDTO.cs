using System;
using Volo.Abp.Application.Dtos;

namespace ConnectVN.Social_Network.Admin.DTO
{
    public class StoryDTO : IEntityDto<Guid>
    {
        public string Story_Title { get; set; }
        public string Story_Synopsis { get; set; }
        public string Img_Url { get; set; }
        public bool IsShow { get; set; }
        public string TagName { get; set; }
        public string NameCategory { get; set; }
        public double Rating { get; set; }
        public Guid Id { get; set; }
    }
}
