using System;
using CodeBase.Services.Data;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Wallet
{
    public class WalletPresenter : IInitializable, IDisposable
    {
        private readonly WalletUI _walletUI;
        private readonly Wallet _wallet;
        private readonly ISaveSystem _saveSystem;

        public WalletPresenter(WalletUI walletUI, Wallet wallet,
            WalletStaticDataService walletStaticDataService, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _walletUI = walletUI;
            _wallet = wallet;
            _wallet.SetMaxMoney(walletStaticDataService.Get().MaxMoney);
        }

        public void Init(int money)
        {
            _walletUI.SetValue(money);
            _wallet.SetInitalMoney(money);
        }
        
        public void Initialize() =>
            _wallet.MoneyChanged += SetMoney;

        public void Dispose() =>
            _wallet.MoneyChanged -= SetMoney;

        private async void SetMoney(int money)
        {
            _walletUI.SetValue(money);
            var worldData = await _saveSystem.Load<WorldData>();
            worldData.PlayerData.Money = money;
        }
    }
}