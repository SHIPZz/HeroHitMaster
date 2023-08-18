using System;
using CodeBase.UI.Wallet;

namespace CodeBase.Services.CheckOut
{
    public class CheckOutService
    {
        private readonly Wallet _wallet;

        public CheckOutService(Wallet wallet)
        {
            _wallet = wallet;
        }

        public event Action Succeeded;

        public void Buy(int price)
        {
            if (!_wallet.TryRemoveMoney(price)) 
                return;
            
            Succeeded?.Invoke();
        }
    }
}