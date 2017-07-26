using System;
using static ActorsDemoWeb.Actors.Actors.Payments.PaymentState;

namespace ActorsDemoWeb.Actors.Actors.Payments
{
    public class PaymentDetailsModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Amount { get; set; }
        public DateTime FinalizedOn { get; set; }
        public PaymentStatus Status { get; set; }
        public string StatusCode { get { return Status.ToString(); } }  
    }
}
