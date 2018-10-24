using Akka.Actor;
using AxCluster.Controllers;
using AxCluster.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxCluster.Actors
{
    public class ProxyActor : ReceiveActor
    {
        public Dictionary<string, RoutePathLoad> LoaderSupervisor = new Dictionary<string, RoutePathLoad>();

        public ProxyActor()
        {
            Receive<RoutePathLoad>(m =>
            {
                if(LoaderSupervisor.Where(c=>c.Key == m.OrgId).FirstOrDefault().Value==null)
                {
                    LoaderSupervisor.Add(m.OrgId, m);
                }
            });

            Receive<ReRoutePathRequest>(m =>
            {
                var loaderLoad = LoaderSupervisor.Where(c => c.Key == m.OrgId).FirstOrDefault().Value;

                if (loaderLoad != null)
                {
                    if (loaderLoad.UpstreamPath == m.UpStreamPath)
                        Sender.Tell(new ReRoutePathResponse { Status = MessageStatus.Success, DownStreamPath = loaderLoad.DownstreamPath, UpStreamPath = loaderLoad.UpstreamPath });
                    else
                        Sender.Tell(new ReRoutePathResponse { Status = MessageStatus.Failure, Message = "Unable to find reroute path with upstream path" });
                }
                else
                    Sender.Tell(new ReRoutePathResponse { Status = MessageStatus.Failure, Message = "Unable to find reroute path with upstream path" });
            });
        }
    }

    public class ProxyWorkerActor : ReceiveActor
    {
        public ProxyWorkerActor()
        {

        }
    }
}
