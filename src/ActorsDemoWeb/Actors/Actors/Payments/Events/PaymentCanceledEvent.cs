using System;
using static ActorsDemoWeb.Actors.Actors.Payments.PaymentState;

namespace ActorsDemoWeb.Actors.Actors.Payments
{
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
}
