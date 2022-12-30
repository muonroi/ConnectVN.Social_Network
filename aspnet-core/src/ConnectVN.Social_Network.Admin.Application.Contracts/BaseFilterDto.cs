using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ConnectVN.Social_Network.Admin
{
    public class BaseFilterDto : PagedResultRequestDto
    {
        /// <summary>
        /// Keyword search
        /// </summary>
        public string Keyword { get; set; }
    }
}
