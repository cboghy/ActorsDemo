namespace ActorsDemoWeb.Actors.Actors.Payments
{
    public class InitializePaymentCommand
    {
        public int Amount { get; }

        public InitializePaymentCommand(int amoutCents)
        {
            Amount = amoutCents;
        }
    }
}
