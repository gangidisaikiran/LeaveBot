﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveBotApi.Models
{
    public class AuthorizationResponse : ApiResponse
    {
        public LeaveTypeModel[] Leave_Types { get; set; }
    }
}