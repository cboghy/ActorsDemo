using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsDemoWeb.Actors.Actors.Shop
{
    public interface IShopService
    {
        void Open();
        void Close();
        void Sell(string articleCode, int quantity);
    }
}
