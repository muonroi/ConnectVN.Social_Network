using System;
using Volo.Abp.Application.Dtos;

namespace ConnectVN.Social_Network.Admin.DTO.Storys
{
    public class CreateUpdateStoryDTO : IEntityDto<Guid>
    {
        public CreateUpdateStoryDTO()
        {

        }
        public CreateUpdateStoryDTO(Guid guidStory, int tagId, int cateGoryId, string story_Title,
                        string story_Synopsi,
                        string img_Url,
                        bool isShow, double rating = 0)
        {
            Id = guidStory;
            Story_Title = story_Title;
            Story_Synopsis = story_Synopsi;
            Img_Url = img_Url;
            IsShow = isShow;
            Rating = rating;
            TagId = tagId;
            CategoryId = cateGoryId;

        }
        public string Story_Title { get; set; }
        public string Story_Synopsis { get; set; }
        public string Img_Url { get; set; }
        public bool IsShow { get; set; }
        public double Rating { get; set; }
        public Guid Id { get; set; }
        public int TagId { get; set; }
        public int CategoryId { get; set; }
    }
}
