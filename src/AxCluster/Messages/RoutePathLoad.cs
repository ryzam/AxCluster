using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using AxCluster.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AxCluster.Messages
{

    public class RoutePathLoad
    {
        public string OrgId { get; set; }
        public string UpstreamPath { get; set; }
        public string DownstreamPath { get; set; }
    }

}