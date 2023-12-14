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
        [SerializeField] private Button _acceptBoughtWeaponButton;
        [SerializeField] private List<ParticleSystem> _effects;
        [SerializeField] private TextMeshProUGUI _purchasedText;
        [SerializeField] private float _targetScaleY = 1f;
        [SerializeField] private float _targetScaleDuration = 0.3f;
        [SerializeField] private Transform _effectPosition;
        [SerializeField] private I2.Loc.Localize _purchasedTextLocalize;

        private AudioSource _purchasedWeaponSound;

        private bool _isAnimating;
        private Dictionary<WeaponTypeId, string> _translatedWeaponNames;
        private Tween _buttonTween;
        private Tween _nameTextTween;
        private Tween _priceTextTween;

        public event Action AdButtonClicked;
        public event Action AcceptWeaponButtonClicked;

        [Inject]
        private void Construct(IProvider<WeaponIconsProvider> provider, ISoundStorage soundStorage)
        {
            _purchasedWeaponSound = soundStorage.Get(SoundTypeId.PurchasedWeapon);
        }

        private void OnEnable()
        {
            _adButton.onClick.AddListener(OnAddButtonClicked);
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _acceptBoughtWeaponButton.onClick.AddListener(OnAcceptBoughtWeaponClicked);
        }

        private void OnDisable()
        {
            _acceptBoughtWeaponButton.onClick.RemoveListener(OnAcceptBoughtWeaponClicked);
            _adButton.onClick.RemoveListener(OnAddButtonClicked);
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
        }

        public void SetTranslatedNames(Dictionary<WeaponTypeId, string> translatedWeaponNames) =>
            _translatedWeaponNames = translatedWeaponNames;

        public void DisableBuyButtons()
        {
            SetButtonScale(_buyButton, false);
            SetButtonScale(_adButton, false);
        }

        public void ShowEffects()
        {
            ParticleSystem randomEffect = _effects[Random.Range(0, _effects.Count - 1)];
            randomEffect.transform.position = _effectPosition.position;
            randomEffect.Play();
            _purchasedWeaponSound.Play();
            DisableBuyButtons();
        }

        public void SetAdWeaponInfo(WeaponData weaponData, bool isBought, int watchedAds, bool isInHand)
        {
            _price.gameObject.SetActive(false);
            _adPrice.gameObject.SetActive(true);
            SetButtonScale(_buyButton, false);
            SetButtonScale(_adButton, false);
            SetButtonScale(_acceptBoughtWeaponButton, false);
            SetWeaponNameInfo(_translatedWeaponNames[weaponData.WeaponTypeId]);

            if (isInHand)
            {
                DisableBuyButtons();
                SetAdWeaponPriceInfo(weaponData, true, watchedAds);
                SetButtonScale(_acceptBoughtWeaponButton, false);
                return;
            }

            if (isBought)
            {
                DisableBuyButtons();
                SetAdWeaponPriceInfo(weaponData, true, watchedAds);
                SetButtonScale(_acceptBoughtWeaponButton, true);
                return;
            }

            SetAdWeaponPriceInfo(weaponData, false, watchedAds);
            SetButtonScale(_adButton, true);
            SetPriceAnimationText(_adPrice.transform);
        }

        public void SetMoneyWeaponInfo(WeaponTypeId weaponTypeId, bool isVisible, bool isBought, bool isInHand)
        {
            _adPrice.gameObject.SetActive(false);
            _price.gameObject.SetActive(true);
            SetButtonScale(_adButton, false);
            SetButtonScale(_buyButton, false);
            SetButtonScale(_acceptBoughtWeaponButton, false);

            SetWeaponNameInfo(_translatedWeaponNames[weaponTypeId]);

            SetPriceAnimationText(_price.transform);

            if (isInHand)
                return;

            if (isBought)
            {
                SetButtonScale(_acceptBoughtWeaponButton, true);
                return;
            }

            if (!isVisible)
            {
                SetButtonScale(_buyButton, false);
                return;
            }

            SetButtonScale(_buyButton, true);
            SetPriceAnimationText(_price.transform);
        }

        public void SetActiveAcceptWeaponButton(bool isActive) =>
            SetButtonScale(_acceptBoughtWeaponButton, isActive);

        public void SetMoneyWeaponPriceInfo(WeaponData weaponData, bool isVisible)
        {
            _purchasedTextLocalize.OnLocalize(true);

            if (!isVisible)
            {
                _price.text = $"<color=#ffdc30> {_purchasedText.text}</color>";
                return;
            }

            _price.text = $"<color=#ffdc30> {weaponData.Price.Value}</color>";
        }

        private void OnAcceptBoughtWeaponClicked()
        {
            ShowEffects();
            SetButtonScale(_acceptBoughtWeaponButton, false);
            AcceptWeaponButtonClicked?.Invoke();
        }

        private void SetPriceAnimationText(Transform targetTransform)
        {
            _priceTextTween?.Kill(true);
            _priceTextTween = DOTween
                .Sequence()
                .Append(targetTransform.DOScaleY(0, _targetScaleDuration))
                .AppendInterval(0.05f)
                .Append(targetTransform.DOScaleY(_targetScaleY, _targetScaleDuration))
                .SetUpdate(true);

            _priceTextTween.Play();
        }

        private void OnBuyButtonClicked() =>
            _buyButton.enabled = false;

        private void OnAddButtonClicked()
        {
            _adButton.enabled = false;
            AdButtonClicked?.Invoke();
        }

        private void SetButtonScale(Button button, bool isVisible)
        {
            button.interactable = isVisible;
            button.enabled = isVisible;
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
                .Append(button.transform.DOScale(Vector3.one, _targetScaleDuration))
                .SetUpdate(true);

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
                .Append(_name.transform.DOScaleY(_targetScaleY, _targetScaleDuration))
                .SetUpdate(true);

            _nameTextTween.Play();
        }

        private void SetAdWeaponPriceInfo(WeaponData weaponData, bool isBought, int watchedAds)
        {
            _purchasedTextLocalize.OnLocalize(true);

            if (isBought)
            {
                _adPrice.text = $"<color=#ffdc30> {_purchasedText.text}</color>";
                return;
            }

            _adPrice.text = $"{watchedAds}/{weaponData.Price.AdQuantity}";
        }
    }
}