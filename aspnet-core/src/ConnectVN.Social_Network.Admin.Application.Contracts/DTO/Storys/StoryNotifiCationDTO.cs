using ConnectVN.Social_Network.Storys;
using System;

namespace ConnectVN.Social_Network.Admin.DTO.Storys
{
    public class StoryNotifiCationDTO
    {
        public Guid UserGuid { get; set; }
        public Guid StoryGuid { get; set; }
        public string NotifiCationUrl { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public EnumStateNotification NotificationSate { get; set; }
        public DateTime ReadNotificationDate { get; set; }
        public string Img_Url { get; set; }
    }
}
