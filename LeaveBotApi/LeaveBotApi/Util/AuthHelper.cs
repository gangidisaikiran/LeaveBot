using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using LeaveBotApi.Models;
using Apiv2.ELeave.Dao;
using System.Net;
using System.Web.Http;
using ELeaveLibrary;
using System.Data.SqlClient;
using System.Data;

namespace LeaveBotApi.Util
{
    public class AuthHelper
    {
        private static LeaveDaoHelper leaveDaoHelper = LeaveDaoHelper.getInstance();

        internal static AuthenticationResponse checkAuthentication(AuthenticationRequest authenticationRequest, HttpRequestMessage request)
        {
            AuthenticationResponse authenticationResponse = new AuthenticationResponse();
            try
            {
                string CompanyGuid = leaveDaoHelper.GetCompanyGUIDByUserID(authenticationRequest.Username);
                if (CompanyGuid != null
                    && CompanyGuid != String.Empty
                    && Apiv2.ELeave.Util.Common.getInstance().EscapeSql(CompanyGuid) == authenticationRequest.Company_Id)
                {
                    AuthDAL.createAuthCode(authenticationRequest.Username, authenticationRequest.Company_Id, CompanyGuid);
                    authenticationResponse.Success = true;
                }
                else
                {
                    authenticationResponse.Success = false;
                    authenticationResponse.Error = new Error
                    {
                        Id = ErrorCode.COMPANY_NOT_MATCHED,
                        Message = ErrorMessage.COMPANY_NOT_MATCHED
                    };
                    throw new HttpResponseException(request.CreateResponse(HttpStatusCode.Unauthorized, authenticationResponse));
                }
            }
            catch (Exception ex)
            {
                authenticationResponse.Success = false;
                authenticationResponse.Error = new Error
                {
                    Id = ErrorCode.INTERNAL_ERROR,
                    Message = ex.Message
                };
                throw new HttpResponseException(request.CreateResponse(HttpStatusCode.InternalServerError, authenticationResponse));
            }
            return authenticationResponse;
        }

        
        internal static bool checkAutherization(HttpRequestMessage request)
        {
            var Fb_Id = request.GetQueryNameValuePairs().First((param) => param.Key == "Fb_Id").Value;
            if (AuthDAL.checkIfSessionExists(Fb_Id))
                return true;
            return false;
        }

        
        internal static AuthorizationResponse authorize(AuthorizationRequest authorizationRequest, HttpRequestMessage request)
        {
            AuthorizationResponse authorizationResponse = new AuthorizationResponse();
            try
            {
                if (AuthDAL.checkIfAuthCodeIsValid(authorizationRequest.Company_Id, authorizationRequest.Username, authorizationRequest.Auth_Code))
                {
                    string CompanyGuid = leaveDaoHelper.GetCompanyGUIDByUserID(authorizationRequest.Username);
                    AuthDAL.createSession(CompanyGuid, authorizationRequest.Username, authorizationRequest.Fb_Id);
                    authorizationResponse.Success = true;
                    authorizationResponse.Leave_Types = LeaveHelper.getLeaveTypes(authorizationRequest.Fb_Id, request).Leave_Types;
                    return authorizationResponse;
                }
                else
                {
                    authorizationResponse.Success = false;
                    authorizationResponse.Error = new Error
                    {
                        Id = ErrorCode.INVALID_AUTH_CODE,
                        Message = ErrorMessage.INVALID_AUTH_CODE
                    };
                    throw new HttpResponseException(request.CreateResponse(HttpStatusCode.Unauthorized, authorizationResponse));
                }
            }
            catch (Exception ex)
            {
                authorizationResponse.Success = false;
                authorizationResponse.Error = new Error
                {
                    Id = ErrorCode.INTERNAL_ERROR,
                    Message = ex.Message
                };
                throw new HttpResponseException(request.CreateResponse(HttpStatusCode.InternalServerError, authorizationResponse));
            }
        }
    }
}