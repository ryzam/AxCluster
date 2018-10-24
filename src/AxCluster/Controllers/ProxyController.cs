using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using AxCluster.Managers;
using AxCluster.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AxCluster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProxyController : ControllerBase
    {
        public ProxyController(AxClusterManager axClusterManager)
        {
            AxClusterManager = axClusterManager;
        }

        public AxClusterManager AxClusterManager { get; }

        [HttpPost]
        public IActionResult Post(RoutePathLoad msg)
        {
            AxClusterManager.ProxyActor.Tell(msg);
            return Ok("Success setup reroute path");
        }
    }

}