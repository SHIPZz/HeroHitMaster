using CodeBase.Services.SaveSystems.Data;
using CodeBase.UI.Wallet;
using CodeBase.UI.Windows.Shop;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Services.UI
{
    public class UIService
    {
        private readonly Canvas _mainUi;
        private readonly ShopWeaponInfoView _shopWeaponInfoView;
        private readonly WalletPresenter _walletPresenter;
        private readonly ShopWeaponPresenter _shopWeaponPresenter;

        public UIService(Canvas mainUi, ShopWeaponInfoView shopWeaponInfoView, WalletPresenter walletPresenter, ShopWeaponPresenter shopWeaponPresenter)
        {
            _mainUi = mainUi;
            _shopWeaponInfoView = shopWeaponInfoView;
            _walletPresenter = walletPresenter;
            _shopWeaponPresenter = shopWeaponPresenter;
        }

        public void Init(WorldData worldData)
        {
            _mainUi.transform.SetParent(null);
            _shopWeaponInfoView.SetTranslatedNames(worldData.TranslatedWeaponNameData.Names);
            _walletPresenter.Init(worldData.PlayerData.Money);
            _shopWeaponPresenter.Init(worldData.PlayerData.LastNotPopupWeaponId);
        }

        public void EnableMainUI()
        {
            _mainUi.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        }
    }
}