using ActorsDemoWeb.Actors.Actors.Payments;
using System.Threading.Tasks;

namespace ActorsDemoWeb.Actors.Actors.Shop
{
    public class PaymentService : IPaymentService
    {
        async Task<PaymentDetailsModel> IPaymentService.GetDetails(int id)
        {
            return await ActorManager.Instance.Get<PaymentDetailsModel>(id);
        }

        void IPaymentService.Cancel(int id)
        {
            ActorManager.Instance.SendPaymentCommand(id, new CancelPaymentCommand());
        }

        void IPaymentService.Confirm(int id)
        {
            ActorManager.Instance.SendPaymentCommand(id, new ConfirmPaymentCommand());
        }

        void IPaymentService.NewPayment(int id, int amount)
        {
            ActorManager.Instance.SendPaymentCommand(id, new InitializePaymentCommand(amount));
        }
    }
}
