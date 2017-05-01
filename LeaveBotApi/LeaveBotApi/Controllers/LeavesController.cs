using LeaveBotApi.Models;
using LeaveBotApi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LeaveBotApi.Controllers
{
    public class LeavesController : ApiController
    {
        // GET: Leaves
        [Route("leave_types")]
        [HttpGet]
        public async Task<IHttpActionResult> Index([FromUri]string fb_id)
        {
            return Ok(LeaveHelper.getLeaveTypes(fb_id, Request));
        }

        [HttpGet]
        [Route("working_days")]
        public async Task<IHttpActionResult> GetWorkingDays([FromUri]GetWorkingDaysRequest getLeaveDaysRequest)
        {
            return Ok(LeaveHelper.getWorkingDays(getLeaveDaysRequest, Request));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]LeavePostRequest leavePostRequest)
        {
            return Ok(LeaveHelper.postLeaveRequest(leavePostRequest, Request));
        }
    }
}