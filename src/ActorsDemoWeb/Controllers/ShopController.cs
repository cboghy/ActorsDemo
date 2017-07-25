using ActorsDemoWeb.Actors.Actors.Shop;
using Microsoft.AspNetCore.Mvc;

namespace ActorsDemoWeb.Controllers
{
    [Route("api/[controller]")]
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet("open")]
        public ActionResult Open()
        {
            _shopService.Open();

            return Ok();
        }

        [HttpGet("close")]
        public ActionResult Close()
        {
            _shopService.Close();

            return Ok();
        }

        [HttpGet("sell/{articleCode}/{quantity}")]
        public ActionResult Sell(string articleCode, int quantity)
        {
            _shopService.Sell(articleCode, quantity);

            return Ok();
        }
    }
}
