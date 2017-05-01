using LeaveBotApi.Models;
using LeaveBotApi.Security;
using LeaveBotApi.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LeaveBotApi.Controllers
{
    [OAuthAuthorize]
    public class AuthenticationController : ApiController
    {
        // GET api/values
        [HttpPost]
        [Route("authenticate")]
        public AuthenticationResponse Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            var response = AuthHelper.checkAuthentication(authenticationRequest, Request);
            return response;
        }

        [HttpPost]
        [Route("authorize")]
        public void Authorize([FromBody] AuthorizationRequest authorizationRequest)
        {
            AuthHelper.authorize(authorizationRequest, Request);
        }
    }
}
