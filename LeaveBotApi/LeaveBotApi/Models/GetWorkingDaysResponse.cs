using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveBotApi.Models
{
    public class GetWorkingDaysResponse : ApiResponse
    {
        public int? Days { get; set; }
    }
}