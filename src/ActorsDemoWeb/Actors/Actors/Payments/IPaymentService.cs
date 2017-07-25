using ActorsDemoWeb.Actors.Actors.Payments;
using System.Threading.Tasks;

namespace ActorsDemoWeb.Actors.Actors.Shop
{
    public interface IPaymentService
    {
        Task<PaymentDetailsModel> GetDetails(int id);
        void Cancel(int id);
        void Confirm(int id);
        void NewPayment(int id, int amount);
    }
}
