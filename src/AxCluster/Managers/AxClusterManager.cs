using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxCluster.Managers
{
    public class AxClusterManager
    {
      
        public IActorRef ProxyActor { get; set; }
    }
}
