using Akka.Actor;
using Akka.Persistence;
using System;

namespace ActorsDemoWeb.Actors.Actors.Payments
{
    public partial class PaymentActor : ReceivePersistentActor
    {
        public int Id { get; private set; }
        public override string PersistenceId => $"Pmt-{Id}";

        private PaymentState State { get; set; }

        public PaymentActor(int id)
        {
            Id = id;
            State = new PaymentState();

            Command<InitializePaymentCommand>(command => HandleInitializePaymentCommand(command));
            Command<ConfirmPaymentCommand>(command => HandleConfirmPaymentCommand(command));
            Command<CancelPaymentCommand>(command => HandleCancelPaymentCommand(command));

            Recover<PaymentCreatedEvent>(@event => State.HandleEvent(@event));
            Recover<PaymentConfirmedEvent>(@event => State.HandleEvent(@event));
            Recover<PaymentCanceledEvent>(@event => State.HandleEvent(@event));

            Command<string>(command => HandleStringCommand(command));

        }

        public static Props Props(int id)
        {
            return Akka.Actor.Props.Create(() => new PaymentActor(id));
        }

        private void HandleStringCommand(string commandMessage)
        {

            if (commandMessage.Equals(typeof(PaymentDetailsModel).Name))
            {
                Sender.Tell(new PaymentDetailsModel
                {
                    Id = Id,
                    CreatedOn = State.CreatedOn,
                    Amount = State.Amount,
                    FinalizedOn = State.FinalizedOn,
                    Status = State.Status
                }, Self);
            }

        }

        private void HandleInitializePaymentCommand(InitializePaymentCommand command)
        {
            var @event = new PaymentCreatedEvent(command.Amount);
            Persist(@event, persistedEvent =>
            {
                State.HandleEvent(persistedEvent);
            });
        }

        private void HandleConfirmPaymentCommand(ConfirmPaymentCommand command)
        {
            var @event = new PaymentConfirmedEvent(DateTime.Now);
            Persist(@event, persistedEvent =>
            {
                State.HandleEvent(persistedEvent);
            });
        }

        private void HandleCancelPaymentCommand(CancelPaymentCommand command)
        {
            var @event = new PaymentCanceledEvent(DateTime.Now);
            Persist(@event, persistedEvent =>
            {
                State.HandleEvent(persistedEvent);
            });
        }
        
    }
}
