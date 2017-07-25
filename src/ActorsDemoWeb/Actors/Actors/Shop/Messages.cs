namespace ActorsDemoWeb.Actors.Actors.Shop
{
    public class OpenMessage { }

    public class CloseMessage { }

    public class SaleMessage
    {
        public SaleMessage(string articleCode, int quantity)
        {
            ArticleCode = articleCode;
            Quantity = quantity;
        }

        public string ArticleCode { get; }
        public int Quantity { get; }
    }
}
