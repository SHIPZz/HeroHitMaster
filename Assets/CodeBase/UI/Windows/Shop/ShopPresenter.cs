using System;
using CodeBase.Enums;
using Zenject;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopPresenter : IInitializable, IDisposable
    {
        private readonly ShopView _shopView;
        private readonly WindowService _windowService;
        private readonly ShopMoneyText _shopMoneyText;
        private readonly Wallet.Wallet _wallet;

        public ShopPresenter(ShopView shopView, WindowService windowService,
            ShopMoneyText shopMoneyText, Wallet.Wallet wallet)
        {
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

        private void Open()
        {
            _shopMoneyText.SetMoney(1000);
            _windowService.CloseAll(() => _windowService.Open(WindowTypeId.Shop));
        }

        private void Close() => 
            _windowService.Close(WindowTypeId.Shop, () => _windowService.Open(WindowTypeId.Play));
    }
}