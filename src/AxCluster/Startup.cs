using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using AxCluster.Actors;
using AxCluster.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AxCluster
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ActorSystem actorSystem = ActorSystem.Create("axcluster");

            services.AddActor(actorSystem);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }

    public static class ActorServices
    {
        public static void AddActor(this IServiceCollection services, ActorSystem actorSystem)
        {
            services.AddSingleton<ActorSystem>(t => actorSystem);

            var proxyActor = actorSystem.ActorOf(Props.Create(() => new ProxyActor()));
            AxClusterManager axClusterManager = new AxClusterManager();
            axClusterManager.ProxyActor = proxyActor;

            services.AddSingleton<AxClusterManager>(axClusterManager);
        }
    }
}
