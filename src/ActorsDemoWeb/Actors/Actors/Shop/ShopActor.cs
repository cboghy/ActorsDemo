using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;

namespace ActorsDemoWeb.Actors.Actors.Shop
{
    public class ShopActor: ReceiveActor
    {
        public ShopActor()
        {
            SetOpenBehavior();
        }

        private void SetOpenBehavior()
        {
            Console.WriteLine("switched to open behavior");

            Receive<OpenMessage>(s =>
            {
                Console.WriteLine("i'm already opened!!!!");
            });
            Receive<CloseMessage>(s =>
            {
                Become(SetClosedBehavior);
            });
            Receive<SaleMessage>(s =>
            {
                Console.WriteLine($"selling {s.Quantity} of {s.ArticleCode}");
            });
        }

        private void SetClosedBehavior()
        {
            Console.WriteLine("switched to closed behavior");

            Receive<OpenMessage>(s =>
            {
                Become(SetOpenBehavior);
            });

            Receive<CloseMessage>(s =>
            {
                Console.WriteLine("i'm already closed!!!!");
            });
            Receive<SaleMessage>(s =>
            {
                Console.WriteLine("cannot sell while closed");
            });
        }


        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ShopActor());
        }
    }
}
