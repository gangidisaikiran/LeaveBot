using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveBotApi.Util
{
    public class ErrorCode
    {
        public const string INTERNAL_ERROR = "0";
        public const string COMPANY_NOT_MATCHED = "1";
        public const string INVALID_AUTH_CODE = "2";
    }
}