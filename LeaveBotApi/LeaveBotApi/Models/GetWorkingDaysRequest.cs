using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveBotApi.Models
{
    public class GetWorkingDaysRequest
    {
        public string Fb_id { get; set; }
        public DateTime From_Date { get; set; }
        public string From_Time { get; set; }
        public DateTime To_Date { get; set; }
        public string To_Time { get; set; }
        public int LeaveTypeId { get; set; }
    }
}