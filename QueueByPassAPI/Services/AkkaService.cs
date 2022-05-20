using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.DependencyInjection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using QueueByPassAPI.Actor;
using QueueByPassAPI.Model;

namespace QueueByPassAPI.Services
{
    public interface IActorBridge
    {
        void Tell(string actorName, PostSpec message);        
    }
    public class AkkaService : IHostedService, IActorBridge
    {
        private ActorSystem _actorSystem;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        
        private Dictionary<string,IActorRef> _actors;

        private readonly IHostApplicationLifetime _applicationLifetime;

        public AkkaService(IServiceProvider serviceProvider, IHostApplicationLifetime appLifetime, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _applicationLifetime = appLifetime;
            _configuration = configuration;
            _actors = new Dictionary<string, IActorRef>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            var bootstrap = BootstrapSetup.Create();


            // enable DI support inside this ActorSystem, if needed
            var diSetup = DependencyResolverSetup.Create(_serviceProvider);

            // merge this setup (and any others) together into ActorSystemSetup
            var actorSystemSetup = bootstrap.And(diSetup);

            // start ActorSystem
            _actorSystem = ActorSystem.Create("akka-universe", actorSystemSetup);
            

            // add a continuation task that will guarantee shutdown of application if ActorSystem terminates
            //await _actorSystem.WhenTerminated.ContinueWith(tr => {
            //   _applicationLifetime.StopApplication();
            //});
            _actorSystem.WhenTerminated.ContinueWith(tr => {
                _applicationLifetime.StopApplication();
              });
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // strictly speaking this may not be necessary - terminating the ActorSystem would also work
            // but this call guarantees that the shutdown of the cluster is graceful regardless
            await CoordinatedShutdown.Get(_actorSystem).Run(CoordinatedShutdown.ClrExitReason.Instance);
        }

        public void Tell(string actorName, PostSpec message)
        {
            if(!_actors.ContainsKey(actorName))
            {
                _actors[actorName] = _actorSystem.ActorOf(QueueActor.Props(), actorName);
            }
            
            _actors[actorName].Tell(message);
        }

    }
}
