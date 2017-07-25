using Akka.Actor;
using Akka.Persistence;
using System;

namespace ActorsDemoWeb.Actors.Actors.Payments
{
    public class PaymentActor : ReceivePersistentActor
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

            Recover<PaymentCreatedEvent>(@event => HandleEvent(@event));
            Recover<PaymentConfirmedEvent>(@event => HandleEvent(@event));
            Recover<PaymentCanceledEvent>(@event => HandleEvent(@event));

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
                HandleEvent(persistedEvent);
            });
        }

        private void HandleConfirmPaymentCommand(ConfirmPaymentCommand command)
        {
            var @event = new PaymentConfirmedEvent(DateTime.Now);
            Persist(@event, persistedEvent =>
            {
                HandleEvent(persistedEvent);
            });
        }

        private void HandleCancelPaymentCommand(CancelPaymentCommand command)
        {
            var @event = new PaymentCanceledEvent(DateTime.Now);
            Persist(@event, persistedEvent =>
            {
                HandleEvent(persistedEvent);
            });
        }

        private void HandleEvent(PaymentCreatedEvent @event)
        {
            State.Amount = @event.Amount;
            State.CreatedOn = @event.CreatedOn;
            State.Status = @event.Status;
        }

        private void HandleEvent(PaymentConfirmedEvent @event)
        {
            State.FinalizedOn = @event.ConfirmedOn;
            State.Status = @event.Status;
        }

        private void HandleEvent(PaymentCanceledEvent @event)
        {
            State.FinalizedOn = @event.CanceledOn;
            State.Status = @event.Status;
        }


        public class PaymentCreatedEvent
        {
            public int Amount { get; private set; }
            public DateTime CreatedOn { get; private set; }
            public PaymentStatus Status { get; private set; }

            public PaymentCreatedEvent(int amount)
            {
                Amount = amount;
                CreatedOn = DateTime.Now;
                Status = PaymentStatus.Pending;
            }
        }

        public class PaymentConfirmedEvent
        {
            public DateTime ConfirmedOn { get; private set; }
            public PaymentStatus Status { get; private set; }

            public PaymentConfirmedEvent(DateTime confirmedOn)
            {
                ConfirmedOn = confirmedOn;
                Status = PaymentStatus.Confirmed;
            }
        }
        
        public class PaymentCanceledEvent
        {
            public DateTime CanceledOn { get; private set; }
            public PaymentStatus Status { get; private set; }

            public PaymentCanceledEvent(DateTime canceledOn)
            {
                CanceledOn = canceledOn;
                Status = PaymentStatus.Canceled;
            }
        }


        public class PaymentState
        {
            public DateTime CreatedOn { get; set; }
            public DateTime FinalizedOn { get; set; }
            public int Amount { get; set; }            
            public PaymentStatus Status { get; set; }
        }


        public enum PaymentStatus
        {
            Pending,
            Confirmed,
            Canceled
        }

    }
}
