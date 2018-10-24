using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using AxCluster.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AxCluster.Controllers
{
    [ApiController]
    public class RouterController : ControllerBase
    {
        public RouterController(AxClusterManager axClusterManager)
        {
            AxClusterManager = axClusterManager;
        }

        public AxClusterManager AxClusterManager { get; }

        [HttpGet]
        [Route("{orgid}/{upstreampath}")]
        public async Task<IActionResult> Get(string orgid,string upstreampath)
        {
            var reRoutePathResponse = await AxClusterManager.ProxyActor.Ask<ReRoutePathResponse>(new ReRoutePathRequest { OrgId = orgid , UpStreamPath = upstreampath});
            if (reRoutePathResponse.Status == MessageStatus.Success)
                return Ok(reRoutePathResponse);
            else
                return BadRequest(reRoutePathResponse.Message);
        }
    }

    public enum MessageStatus
    {
        Success, Failure
    }
    public abstract class BaseResponseMessage
    {
        public MessageStatus Status { get; set; }
        public string Message { get; set; }
    }
    public class ReRoutePathResponse :BaseResponseMessage
    {
        public string UpStreamPath { get; set; }
        public string DownStreamPath { get; set; }
    }

    public class ReRoutePathRequest
    {
        public string OrgId { get; set; }
        public string UpStreamPath { get; set; }
    }
}