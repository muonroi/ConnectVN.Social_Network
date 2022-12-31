using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ConnectVN.Social_Network.Admin.DTO
{
    public class CreateUpdateStoryDTO : IEntityDto<Guid>
    {
        public string Story_Title { get; set; }
        public string Story_Synopsis { get; set; }
        public string Img_Url { get; set; }
        public bool IsShow { get; set; }
        public Guid Id { get; set; }
    }
}
