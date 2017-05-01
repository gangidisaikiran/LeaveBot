using ELeaveLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LeaveBotApi.Util
{
    public class AuthDAL
    {
        public static bool checkIfSessionExists(string fb_Id)
        {
            try
            {
                DataSet ds = new DBUtility().ReturnDataSet("select top 1 * from APISessions where ExternalID=@p_FBId and ExpiryDate > GETDATE()", new SqlParameter[1]
                  {
                    new SqlParameter("@p_FBId", (object) fb_Id),
                  }, CommandType.Text);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        public static DataRow getSession(string fb_Id)
        {
            try
            {
                DataSet ds = new DBUtility().ReturnDataSet("select top 1 * from APISessions where ExternalID=@p_FBId and ExpiryDate > GETDATE()", new SqlParameter[1]
                  {
                    new SqlParameter("@p_FBId", (object) fb_Id),
                  }, CommandType.Text);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0];
                }
                return null;
            }
            catch
            {
                throw;
            }
        }


        public static bool checkIfAuthCodeIsValid(string CompanyID, string UserID, string ApiAuthCode)
        {
            try
            {
                DataSet ds = new DBUtility().ReturnDataSet("select top 1 * from ApiAuthCode where CompanyID=@p_CompanyID and UserID=@p_UserID and IsUsed=0 and ApiAuthCode=@p_ApiAuthCode", new SqlParameter[3]
                  {
                    new SqlParameter("@p_CompanyID", (object) CompanyID),
                    new SqlParameter("@p_UserID", (object) UserID),
                    new SqlParameter("@p_ApiAuthCode", (object) ApiAuthCode)
                  }, CommandType.Text);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    new DBUtility().ExecuteNonQuery("Update ApiAuthCode set IsUsed = 1 where  CompanyID=@p_CompanyID and UserID=@p_UserID and IsUsed=0 and ApiAuthCode=@p_ApiAuthCode", new SqlParameter[3]
                      {
                        new SqlParameter("@p_CompanyID", (object) CompanyID),
                        new SqlParameter("@p_UserID", (object) UserID),
                        new SqlParameter("@p_ApiAuthCode", (object) ApiAuthCode)
                      }, CommandType.Text);
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        public static void createAuthCode(string UserID, string CompanyId, string CompanyGuid)
        {
            string AuthCode = new Random()
                .Next((int)Math.Pow(10, 6))
                .ToString();
            new DBUtility().ExecuteNonQuery("INSERT INTO APIAuthCode (AuthCode, CompanyID, UserID, DateTimeCreated) values (@p_AuthCode, @p_CompanyID, @p_UserID, GETDATE())", new SqlParameter[3]
              {
                new SqlParameter("@p_UserID", (object) UserID),
                new SqlParameter("@p_CompanyID", (object) CompanyId),
                new SqlParameter("@p_AuthCode", (object) AuthCode)
              }, CommandType.Text);
            string fromMail = new EmailEngineSupportDAL().Get_LeaveEmailFromName(CompanyGuid).Tables[0].Rows[0]["fromemailaddr"].ToString(); ;
            new EmailHelper
            {
                To = "",
                From = fromMail
            }.send("Leave Bot Authentication", string.Format("Here is your Authorization code: {0}", AuthCode));
        }

        internal static void createSession(string companyGuid, string username, string fb_Id)
        {
            string sessionId = Guid.NewGuid().ToString();
            int ExpiryDays = int.Parse(ConfigurationManager.AppSettings["ExpirationPeriod"] ?? "0");
            DateTime? temp = new DateTime().AddDays(ExpiryDays);
            DateTime? expiryDate = ExpiryDays == 0 ? null : temp;
            new DBUtility().ExecuteNonQuery("INSERT INTO APISessions (SessionID, CompanyGUID, UserGUID, ExpiryDate, ExternalID) values (@p_SessionId, @p_CompanyGuid, @p_UserGuid, @p_expiryDate, @p_FbId)", new SqlParameter[5]
              {
                new SqlParameter("@p_SessionId", (object) sessionId),
                new SqlParameter("@p_CompanyGuid", (object) companyGuid),
                new SqlParameter("@p_UserGuid", (object) username),
                new SqlParameter("@p_FbId", (object) fb_Id),
                new SqlParameter("@p_expiryDate", (object) fb_Id),
              }, CommandType.Text);
        }
    }
}