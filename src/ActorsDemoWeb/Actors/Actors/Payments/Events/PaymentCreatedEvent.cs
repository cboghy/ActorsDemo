using System;
using static ActorsDemoWeb.Actors.Actors.Payments.PaymentState;

namespace ActorsDemoWeb.Actors.Actors.Payments
{
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
}
