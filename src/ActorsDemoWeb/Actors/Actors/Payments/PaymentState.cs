using System;
using static ActorsDemoWeb.Actors.Actors.Payments.PaymentActor;

namespace ActorsDemoWeb.Actors.Actors.Payments
{
    public class PaymentState
    {
        public DateTime CreatedOn { get; private set; }
        public DateTime FinalizedOn { get; private set; }
        public int Amount { get; private set; }
        public PaymentStatus Status { get; private set; }


        public void HandleEvent(PaymentCreatedEvent @event)
        {
            Amount = @event.Amount;
            CreatedOn = @event.CreatedOn;
            Status = @event.Status;
        }

        public void HandleEvent(PaymentConfirmedEvent @event)
        {
            FinalizedOn = @event.ConfirmedOn;
            Status = @event.Status;
        }

        public void HandleEvent(PaymentCanceledEvent @event)
        {
            FinalizedOn = @event.CanceledOn;
            Status = @event.Status;
        }


        public enum PaymentStatus
        {
            Pending,
            Confirmed,
            Canceled
        }
    }
}
