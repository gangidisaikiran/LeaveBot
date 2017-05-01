using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveBotApi.Models
{
    public class LeavePostRequest
    {
        public string Fb_Id { get; set; }
        public string Leave_Type {get; set; }
        public DateTime Date_From { get; set; }
        public string Date_From_Period { get; set; }
        public DateTime Date_To { get; set; }
        public string Date_To_Period { get; set; }
        public string attachments { get; set; }
        public string remarks { get; set; }
    }
}