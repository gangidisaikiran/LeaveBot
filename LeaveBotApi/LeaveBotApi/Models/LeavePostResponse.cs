using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveBotApi.Models
{
    public class LeavePostResponse : ApiResponse
    {
        public string Approver { get; set; }
    }
}