using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.ScriptableObjects.Weapon;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Sound;
using CodeBase.UI.Weapons;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopWeaponInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private TextMeshProUGUI _adPrice;
        [SerializeField] private Button _adButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private List<ParticleSystem> _effects;
        [SerializeField] private TextMeshProUGUI _purchasedText;
        [SerializeField] private float _targetScaleY = 1f;
        [SerializeField] private float _targetScaleDuration = 0.3f;

        private Dictionary<WeaponTypeId, Image> _shopWeaponIcons;
        private AudioSource _purchasedWeaponSound;

        public event Action AdButtonClicked;

        private bool _isAnimating;
        private Dictionary<WeaponTypeId, string> _translatedWeaponNames;
        private Tween _buttonTween;
        private Tween _nameTextTween;
        private Tween _priceTextTween;

        [Inject]
        private void Construct(IProvider<WeaponIconsProvider> provider,
            ISoundStorage soundStorage)
        {
            _shopWeaponIcons = provider.Get().ShopWeaponIcons;
            _purchasedWeaponSound = soundStorage.Get(SoundTypeId.PurchasedWeapon);
        }

        private void OnEnable()
        {
            _adButton.onClick.AddListener(OnAddButtonClicked);
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        }

        private void OnDisable()
        {
            _adButton.onClick.RemoveListener(OnAddButtonClicked);
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        }

        public void SetTranslatedNames(Dictionary<WeaponTypeId, string> translatedWeaponNames) =>
            _translatedWeaponNames = translatedWeaponNames;

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

        public void SetAdWeaponInfo(WeaponData weaponData, bool isBought, int watchedAds)
        {
            _price.gameObject.SetActive(false);
            _adPrice.gameObject.SetActive(true);
            SetButtonScale(_buyButton, false, false);
            SetWeaponNameInfo(_translatedWeaponNames[weaponData.WeaponTypeId]);

            if (isBought)
            {
                DisableBuyButtons();
                SetAdWeaponPriceInfo(weaponData, true, watchedAds);
                return;
            }

            SetAdWeaponPriceInfo(weaponData, false, watchedAds);
            SetButtonScale(_adButton, true, true);
            SetPriceAnimationText(_adPrice.transform);
        }

        public void SetMoneyWeaponInfo(WeaponTypeId weaponTypeId, bool isVisible)
        {
            _adPrice.gameObject.SetActive(false);
            SetButtonScale(_adButton, false, false);
            _price.gameObject.SetActive(true);

            SetWeaponNameInfo(_translatedWeaponNames[weaponTypeId]);

            SetPriceAnimationText(_price.transform);

            if (!isVisible)
            {
                SetButtonScale(_buyButton, false, false);
                return;
            }

            SetButtonScale(_buyButton, true, true);
            SetPriceAnimationText(_price.transform);
        }

        private void SetPriceAnimationText(Transform targetTransform)
        {
            _priceTextTween?.Kill(true);
            _priceTextTween = DOTween
                .Sequence()
                .Append(targetTransform.DOScaleY(0, _targetScaleDuration))
                .AppendInterval(0.05f)
                .Append(targetTransform.DOScaleY(_targetScaleY, _targetScaleDuration));

            _priceTextTween.Play();
        }

        private void OnBuyButtonClicked() => 
            _buyButton.enabled = false;

        private void OnAddButtonClicked()
        {
            _adButton.enabled = false;
            AdButtonClicked?.Invoke();
        }

        private void SetButtonScale(Button button, bool isInteractable, bool isVisible)
        {
            button.interactable = isInteractable;
            button.enabled = isInteractable;
            _buttonTween?.Kill(true);

            if (!isVisible)
            {
                button.transform.localScale = Vector3.zero;
                return;
            }

            _buttonTween = DOTween
                .Sequence()
                .Append(button.transform.DOScale(Vector3.zero, _targetScaleDuration))
                .AppendInterval(0.05f)
                .Append(button.transform.DOScale(Vector3.one, _targetScaleDuration));

            _buttonTween.Play();
        }

        private void SetWeaponNameInfo(string name)
        {
            _name.text = name;
            _nameTextTween?.Kill(true);
            _nameTextTween = DOTween
                .Sequence()
                .Append(_name.transform.DOScaleY(0, _targetScaleDuration))
                .AppendInterval(0.05f)
                .Append(_name.transform.DOScaleY(_targetScaleY, _targetScaleDuration));

            _nameTextTween.Play();
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

        public void SetMoneyWeaponPriceInfo(WeaponData weaponData, bool isVisible)
        {
            if (!isVisible)
            {
                _price.text = $"<color=#ffdc30> {_purchasedText.text}</color>";
                return;
            }

            _price.text = $"<color=#ffdc30> {weaponData.Price.Value}</color>";
        }
    }
}