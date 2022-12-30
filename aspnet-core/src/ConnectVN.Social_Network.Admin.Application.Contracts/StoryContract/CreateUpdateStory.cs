using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectVN.Social_Network.Admin.StoryContract
{
    public class CreateUpdateStory
    {
        public string Story_Title { get; set; }
        public string Story_Synopsis { get; set; }
        public string Img_Url { get; set; }
        public bool IsShow { get; set; }
    }
}
