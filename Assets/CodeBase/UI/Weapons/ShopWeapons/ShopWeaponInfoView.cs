using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Data;
using CodeBase.Services.Providers;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using CodeBase.Services.Storages.Sound;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponInfoView : MonoBehaviour
    {
        [SerializeField] private float _scaleIncrease = 0.1f;
        [SerializeField] private float _scaleIncreaseDelay = 0.2f;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private TextMeshProUGUI _adPrice;
        [SerializeField] private Button _adButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private List<ParticleSystem> _effects;
        [SerializeField] private TextMeshProUGUI _purchasedText;

        private Dictionary<WeaponTypeId, Image> _shopWeaponIcons;
        private AudioSource _purchasedWeaponSound;
        private ISaveSystem _saveSystem;

        public event Action AdButtonClicked;

        [Inject]
        private void Construct(IProvider<WeaponIconsProvider> provider,
            ISoundStorage soundStorage,
            ISaveSystem saveSystem, WeaponStaticDataService weaponStaticDataService)
        {
            _saveSystem = saveSystem;
            _shopWeaponIcons = provider.Get().ShopWeaponIcons;
            _purchasedWeaponSound = soundStorage.Get(SoundTypeId.PurchasedWeapon);
        }

        private void OnEnable() =>
            _adButton.onClick.AddListener(OnAddButtonClicked);

        private void OnDisable() =>
            _adButton.onClick.RemoveListener(OnAddButtonClicked);

        public void DisableBuyButtons()
        {
            SetButtonScale(_buyButton, false, false);
            SetButtonScale(_adButton, false, false);
        }

        public void ShowEffectOnPurchasedWeapon(WeaponTypeId weaponDataWeaponTypeId)
        {
            ParticleSystem randomEffect = _effects[Random.Range(0, _effects.Count - 1)];
            randomEffect.transform.position = _shopWeaponIcons[weaponDataWeaponTypeId].transform.position;
            randomEffect.Play();
            _purchasedWeaponSound.Play();
            DisableBuyButtons();
        }

        public async void SetAdWeaponInfo(WeaponData weaponData, bool isBought, int watchedAds)
        {
            var translatedWeaponNameData = await _saveSystem.Load<TranslatedWeaponNameData>();
            _price.gameObject.SetActive(false);
            _adPrice.gameObject.SetActive(true);

            SetWeaponNameInfo(translatedWeaponNameData.Names[weaponData.WeaponTypeId]);

            if (isBought)
            {
                DisableBuyButtons();
                SetAdWeaponPriceInfo(weaponData, true, watchedAds);
                return;
            }

            SetAdWeaponPriceInfo(weaponData, false, watchedAds);
            SetButtonScale(_adButton, true, true);
            SetButtonScale(_buyButton, false, false);
            SetPriceAnimationText(_adPrice.transform);
        }

        private async void SetPriceAnimationText(Transform targetTransform)
        {
            await targetTransform.transform.DOScaleY(0, _scaleIncrease).AsyncWaitForCompletion();
            await UniTask.WaitForSeconds(_scaleIncreaseDelay);
            await targetTransform.transform.DOScaleY(1, _scaleIncrease).AsyncWaitForCompletion();
        }

        public async void SetMoneyWeaponInfo(WeaponData weaponData, bool isBought)
        {
            var translatedWeaponNameData = await _saveSystem.Load<TranslatedWeaponNameData>();

            while (translatedWeaponNameData.Names.Count < 1)
            {
                await UniTask.Yield();
            }

            _adPrice.gameObject.SetActive(false);
            SetButtonScale(_adButton, false, false);
            _price.gameObject.SetActive(true);

            SetWeaponNameInfo(translatedWeaponNameData.Names[weaponData.WeaponTypeId]);
            
            if (isBought)
            {
                SetButtonScale(_buyButton, false, false);
                SetMoneyWeaponPriceInfo(weaponData, isBought);
                return;
            }

            SetButtonScale(_buyButton, true, true);
            SetMoneyWeaponPriceInfo(weaponData, false);
            SetPriceAnimationText(_price.transform);
        }

        private void OnAddButtonClicked() =>
            AdButtonClicked?.Invoke();

        private bool _isAnimating;

        private async void SetButtonScale(Button button, bool isInteractable, bool isVisible)
        {
            button.interactable = isInteractable;
            button.enabled = isInteractable;

            if (!isVisible)
            {
                button.transform.localScale = Vector3.zero;
                return;
            }

            if (button.transform.localScale != Vector3.zero)
                await button.transform.DOScale(Vector3.zero, _scaleIncrease).AsyncWaitForCompletion();

            await UniTask.WaitForSeconds(_scaleIncreaseDelay);
            await button.transform.DOScale(Vector3.one, _scaleIncrease).AsyncWaitForCompletion();
        }

        private async void SetWeaponNameInfo(string name)
        {
            _name.text = name;

            await _name.transform.DOScaleY(0, _scaleIncrease).AsyncWaitForCompletion();
            await UniTask.WaitForSeconds(_scaleIncreaseDelay);
            await _name.transform.DOScaleY(1, _scaleIncrease).AsyncWaitForCompletion();
        }

        private void SetAdWeaponPriceInfo(WeaponData weaponData, bool isBought, int watchedAds)
        {
            if (isBought)
            {
                _adPrice.text = $"<color=#ffdc30> {_purchasedText.text}</color>";
                return;
            }

            _adPrice.text = $"{watchedAds}/{weaponData.Price.AdQuantity}";
        }

        private void SetMoneyWeaponPriceInfo(WeaponData weaponData, bool isBought)
        {
            if (isBought)
            {
                _price.text = $"<color=#ffdc30> {_purchasedText.text}</color>";
                return;
            }

            _price.text = $"<color=#ffdc30> {weaponData.Price.Value}</color>";
        }
    }
}