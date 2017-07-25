namespace ActorsDemoWeb.Actors.Actors.Shop
{
    public class ShopService : IShopService
    {
        void IShopService.Close()
        {
            ActorManager.Instance.SendMessageToShop(new CloseMessage());
        }

        void IShopService.Open()
        {
            ActorManager.Instance.SendMessageToShop(new OpenMessage());
        }

        void IShopService.Sell(string articleCode, int quantity)
        {
            ActorManager.Instance.SendMessageToShop(new SaleMessage(articleCode, quantity));
        }
    }
}
