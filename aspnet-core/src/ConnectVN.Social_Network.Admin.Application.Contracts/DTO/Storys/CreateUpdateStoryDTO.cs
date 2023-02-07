using Microsoft.AspNetCore.Http;
using System;
using Volo.Abp.Application.Dtos;

namespace ConnectVN.Social_Network.Admin.DTO.Storys
{
    public class CreateUpdateStoryDTO : IEntityDto<Guid>
    {
        public CreateUpdateStoryDTO()
        {

        }
        public CreateUpdateStoryDTO(Guid guidStory, int newTagId, int newCateGoryId, string story_Title,
                        string story_Synopsi,
                        bool isShow, string slug, double rating = 0)
        {
            Id = guidStory;
            Story_Title = story_Title;
            Story_Synopsis = story_Synopsi;
            IsShow = isShow;
            Rating = rating;
            NewTagId = newTagId;
            NewCategoryId = newCateGoryId;
            Slug = slug;
        }
        public string Story_Title { get; set; }
        public string Story_Synopsis { get; set; }
        public bool IsShow { get; set; }
        public double Rating { get; set; }
        public IFormFile FormFile { get; set; }
        public Guid Id { get; set; }
        public int OldTagId { get; set; }
        public int OldCategoryId { get; set; }
        public int NewTagId { get; set; }
        public int NewCategoryId { get; set; }
        public string Slug { get; set; }
    }
}
