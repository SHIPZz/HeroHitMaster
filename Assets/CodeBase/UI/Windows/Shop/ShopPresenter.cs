using System;
using CodeBase.Enums;
using Zenject;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopPresenter : IInitializable, IDisposable
    {
        private readonly ShopView _shopView;
        private readonly WindowService _windowService;

        public ShopPresenter(ShopView shopView, WindowService windowService)
        {
            _shopView = shopView;
            _windowService = windowService;
        }

        public void Initialize()
        {
            _shopView.OpenedButtonClicked += Open;
            _shopView.ClosedButtonClicked += Close;
        }

        public void Dispose()
        {
            _shopView.OpenedButtonClicked -= Open;
            _shopView.ClosedButtonClicked -= Close;
        }

        private void Open() => 
            _windowService.Open(WindowTypeId.Shop);

        private void Close() => 
            _windowService.Close(WindowTypeId.Shop);
    }
}