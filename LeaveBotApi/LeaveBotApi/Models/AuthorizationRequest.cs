using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveBotApi.Models
{
    public class AuthorizationRequest
    {
        public string Fb_Id { get; set; }
        public string Company_Id { get; set; }
        public string Username { get; set; }
        public string Auth_Code { get; set; }
    }
}