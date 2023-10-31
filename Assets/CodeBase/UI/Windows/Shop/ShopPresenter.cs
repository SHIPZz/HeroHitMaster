using System;
using CodeBase.Enums;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using Zenject;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopPresenter : IInitializable, IDisposable
    {
        private readonly ShopView _shopView;
        private readonly WindowService _windowService;
        private readonly ShopMoneyText _shopMoneyText;
        private readonly Wallet.Wallet _wallet;
        private readonly ISaveSystem _saveSystem;

        public ShopPresenter(ShopView shopView, WindowService windowService,
            ShopMoneyText shopMoneyText, Wallet.Wallet wallet, ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _wallet = wallet;
            _shopMoneyText = shopMoneyText;
            _shopView = shopView;
            _windowService = windowService;
        }

        public void Initialize()
        {
            _shopView.OpenedButtonClicked += Open;
            _shopView.ClosedButtonClicked += Close;
            _wallet.MoneyChanged += _shopMoneyText.SetMoney;
        }

        public void Dispose()
        {
            _wallet.MoneyChanged -= _shopMoneyText.SetMoney;
            _shopView.OpenedButtonClicked -= Open;
            _shopView.ClosedButtonClicked -= Close;
        }

        private async void Open()
        {
            var worldData = await _saveSystem.Load<WorldData>();
            _shopMoneyText.SetMoney(worldData.PlayerData.Money);
            _windowService.CloseAll(() => _windowService.Open(WindowTypeId.Shop));
        }

        private void Close() => 
            _windowService.Close(WindowTypeId.Shop, () => _windowService.Open(WindowTypeId.Play));
    }
}