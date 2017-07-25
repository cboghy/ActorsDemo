using ActorsDemoWeb.Actors.Actors.Shop;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ActorsDemoWeb.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var details = await _paymentService.GetDetails(id);

            return Ok(details);
        }

        [HttpGet("cancel/{id}")]
        public ActionResult Cancel(int id)
        {
            _paymentService.Cancel(id);

            return Ok();
        }

        [HttpGet("confirm/{id}")]
        public ActionResult Close(int id)
        {
            _paymentService.Confirm(id);

            return Ok();
        }

        [HttpGet("new/{id}/{amount}")]
        public ActionResult Sell(int id, int amount)
        {
            _paymentService.NewPayment(id, amount);

            return Ok();
        }
    }
}
