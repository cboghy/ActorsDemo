using System;
using static ActorsDemoWeb.Actors.Actors.Payments.PaymentState;

namespace ActorsDemoWeb.Actors.Actors.Payments
{
    public partial class PaymentActor
    {
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

    }
}
