using System;
using CodeBase.Services.Data;
using Zenject;

namespace CodeBase.UI.Wallet
{
    public class WalletPresenter : IInitializable, IDisposable
    {
        private readonly WalletUI _walletUI;
        private readonly Wallet _wallet;

        public WalletPresenter(WalletUI walletUI, Wallet wallet, WalletStaticDataService walletStaticDataService)
        {
            _walletUI = walletUI;
            _wallet = wallet;
            _wallet.SetMaxMoney(walletStaticDataService.Get().MaxMoney);
            _walletUI.SetValue(1000);
        }

        public void Initialize()
        {
            _wallet.MoneyChanged += _walletUI.SetValue;
        }

        public void Dispose()
        {
            _wallet.MoneyChanged -= _walletUI.SetValue;
        }
    }
}