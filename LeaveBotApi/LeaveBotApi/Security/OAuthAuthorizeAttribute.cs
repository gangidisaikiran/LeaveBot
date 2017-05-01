using LeaveBotApi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LeaveBotApi.Security
{
    public class OAuthAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                //Check for Secret and Check the session
                if (AuthHelper.checkAutherization(actionContext.Request))
                {
                    var exception = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    throw new HttpResponseException(exception);
                }
                base.OnAuthorization(actionContext);
            }
            catch (Exception ex)
            {
                var exception = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                throw new HttpResponseException(exception);
            }
        }
    }
}