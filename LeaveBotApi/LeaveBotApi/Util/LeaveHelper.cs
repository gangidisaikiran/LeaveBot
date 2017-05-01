using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveBotApi.Models;
using System.Net.Http;
using System.Web.Http;
using ELeaveLibrary;
using System.Data;
using System.Net;

namespace LeaveBotApi.Util
{
    public class LeaveHelper
    {
        internal static LeaveTypesResponse getLeaveTypes(string fb_id, HttpRequestMessage request)
        {
            LeaveTypesResponse response= new LeaveTypesResponse();
            try
            {
                var sessionDetails = AuthDAL.getSession(fb_id);
                var leaveDataTable = new UserDAL().GenerateLeaveTypes_ByUserGUID(sessionDetails["CompanyGUID"] as string, sessionDetails["UserGUID"] as string, new DateTime().Year);
                var leaveTypes = new List<LeaveTypeModel>();
                foreach (DataRow row in leaveDataTable.Rows)
                {
                    leaveTypes.Add(new LeaveTypeModel
                    {
                        LeaveType = (row["LeaveType"] ?? row[0]) as string,
                        LeaveName = (row["LeaveName"] ?? row[1]) as string,
                        IfPerIncident = row[2] as string,
                    });
                }
                response.Success = true;
                response.Leave_Types = leaveTypes.ToArray();
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new Error
                {
                    Id = ErrorCode.INTERNAL_ERROR,
                    Message = ex.Message
                };
                throw new HttpResponseException(request.CreateResponse(HttpStatusCode.InternalServerError, response));
            }


        }

        internal static GetWorkingDaysResponse getWorkingDays(GetWorkingDaysRequest getLeaveDaysRequest, HttpRequestMessage request)
        {
            GetWorkingDaysResponse response = new GetWorkingDaysResponse();
            try
            {
                LeaveDAL leaveDAL = new LeaveDAL();
                var sessionDetails = AuthDAL.getSession(getLeaveDaysRequest.Fb_id);
                var leaveDaysDataSet = leaveDAL.Employee_GetLeaveDay(
                    getLeaveDaysRequest.From_Date,
                    getLeaveDaysRequest.To_Date,
                    getLeaveDaysRequest.From_Time,
                    getLeaveDaysRequest.To_Time,
                    sessionDetails["UserGUID"] as string,
                    sessionDetails["CompanyGUID"] as string
                    );
                // ToDo: Calculate the current balance and check if the required leaves is within the range
                //leaveDAL.Get_CurrentBalance(
                //    sessionDetails["UserGUID"] as string,
                //    getLeaveDaysRequest.LeaveTypeId,
                //    new DateTime().Year,
                //    );
                response.Success = true;
                response.Days = leaveDaysDataSet.Tables[0].Rows[0]["LeaveDays"] as int?;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = new Error
                {
                    Id = ErrorCode.INTERNAL_ERROR,
                    Message = ex.Message
                };
                throw new HttpResponseException(request.CreateResponse(HttpStatusCode.InternalServerError, response));
            }

        }

        internal static LeavePostResponse postLeaveRequest(LeavePostRequest leavePostRequest, HttpRequestMessage request)
        {
            throw new NotImplementedException();
        }
    }
}