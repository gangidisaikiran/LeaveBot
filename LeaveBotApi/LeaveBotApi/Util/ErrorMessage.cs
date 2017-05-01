using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveBotApi.Util
{
    public class ErrorMessage
    {
        public const string COMPANY_NOT_MATCHED = "Mismatch between and UserId and CompanyId provided.";
        public const string INVALID_AUTH_CODE = "Invalid Authorization code.";
    }
}