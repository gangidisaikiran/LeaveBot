using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveBotApi.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public Error Error { get; set; }
    }
}