using System;

using Akka.Actor;
using Akka.Event;

using QueueByPassAPI.Adapter;
using QueueByPassAPI.Model;

namespace QueueByPassAPI.Actor
{
    public class QueueActor: ReceiveActor
    {
        private readonly ILoggingAdapter log = Context.GetLogger();

        private ApiClient apiClient = new ApiClient();

        public QueueActor()
        {
            ReceiveAsync<PostSpec>(async message => {
                log.Info("Received PostSpec message: {0} {1}", message.host, message.path);
                var data = await apiClient.PostCallBack(message.reqId, message.host, message.path, message.data);
            });
        }
        
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new QueueActor());
        }

    }
}
