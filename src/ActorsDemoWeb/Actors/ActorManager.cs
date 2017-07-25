using Akka.Actor;
using ActorsDemoWeb.Actors.Actors.Shop;
using System.Collections.Generic;
using ActorsDemoWeb.Actors.Actors.Payments;
using System.Threading.Tasks;

namespace ActorsDemoWeb.Actors
{
    public class ActorManager
    {
        private static ActorSystem _actorSystem;
        private static ActorManager _instance;

        private IActorRef _shopActorRef;
        private IDictionary<int, IActorRef> _paymentActors = new Dictionary<int, IActorRef>();

        private ActorManager()
        {
            if (null == _actorSystem)
            {
                _actorSystem = ActorSystem.Create("TestSystem");
            }
        }

        public static ActorManager Instance
        {
            get
            {
                if(null == _instance)
                {
                    _instance = new ActorManager();
                }

                return _instance;
            }
        }


        public void SendMessageToShop<T>(T message)
        {
            EnsureShopActor();

            _shopActorRef.Tell(message);
        }

        private void EnsureShopActor()
        {
            if(null == _shopActorRef)
            {
                _shopActorRef = _actorSystem.ActorOf(ShopActor.Props());
            }
        }

        public void SendPaymentCommand<T>(int id, T command)
        {
            EnsurePaymentActor(id);

            _paymentActors[id].Tell(command);
        }

        public async Task<T> Get<T>(int id)
        {
            EnsurePaymentActor(id);

            return await _paymentActors[id].Ask<T>(typeof(T).Name);
        }

        private void EnsurePaymentActor(int id)
        {
            if (!_paymentActors.ContainsKey(id))
            {
                _paymentActors.Add(id, _actorSystem.ActorOf(PaymentActor.Props(id)));
            }
        }
    }
}
