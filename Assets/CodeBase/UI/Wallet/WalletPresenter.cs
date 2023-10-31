using System;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using Zenject;

namespace CodeBase.UI.Wallet
{
    public class WalletPresenter : IInitializable, IDisposable
    {
        private readonly WalletUI _walletUI;
        private readonly Wallet _wallet;
        private readonly IWorldDataService _worldDataService;

        public WalletPresenter(WalletUI walletUI, Wallet wallet,
            WalletStaticDataService walletStaticDataService, IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
            _walletUI = walletUI;
            _wallet = wallet;
            _wallet.SetMaxMoney(walletStaticDataService.Get().MaxMoney);
        }

        public void Init(int money)
        {
            _walletUI.SetValue(money);
            _wallet.SetInitialMoney(money);
        }
        
        public void Initialize() =>
            _wallet.MoneyChanged += SetMoney;

        public void Dispose() =>
            _wallet.MoneyChanged -= SetMoney;

        private void SetMoney(int money)
        {
            _walletUI.SetValue(money);
            _worldDataService.WorldData.PlayerData.Money = money;
        }
    }
}