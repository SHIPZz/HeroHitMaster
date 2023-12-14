using System;
using System.Collections.Generic;
using Agava.WebUtility;
using CodeBase.Enums;
using CodeBase.Services.Ad;
using CodeBase.Services.Pause;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Sound;
using CodeBase.UI.Weapons;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.UI.Windows.Popup
{
    public class PopupInfoView : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<WeaponTypeId, Image> _whiteFrames;
        [SerializeField] private TextMeshProUGUI _mainWeaponText;
        [SerializeField] private Color _color;
        [SerializeField] private int _chooseCycle;
        [SerializeField] private Button _adButton;
        [SerializeField] private float _chooseDuration;
        [SerializeField] private float _closeWhiteFrameDuration;
        [SerializeField] private float _openWhiteFrameDuration;
        [SerializeField] private float _canvasFadeDuration = 0.5f;
        [SerializeField] private float _canvasNonFadeDuration = 0.25f;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private PopupTimerService _popupTimerService;

        private Dictionary<WeaponTypeId, WeaponSelectorView> _weaponIcons;
        private AudioSource _lastWeaponSelectedSound;
        private AudioSource _chooseWeaponSound;
        private List<WeaponSelectorView> _randomIcons = new();
        private Image _lastFrame;
        private int _count;
        private WeaponSelectorView _lastWeaponSelectorView;
        private List<ParticleSystem> _selectedWeaponEffects;
        private Coroutine _chooseRandomWeaponCoroutine;
        private bool _lastWeaponSelected;
        private IPauseService _pauseService;
        private bool _hasFocus;
        private IAdService _adService;

        public event Action AdButtonClicked;
        public event Action<WeaponTypeId> LastWeaponSelected;

        [Inject]
        private void Construct(IProvider<WeaponIconsProvider> provider, ISoundStorage soundStorage,
            EffectsProvider effectsProvider, IPauseService pauseService,
            IAdService adService)
        {
            _adService = adService;
            _pauseService = pauseService;
            _lastWeaponSelectedSound = soundStorage.Get(SoundTypeId.SelectedWeapon);
            _chooseWeaponSound = soundStorage.Get(SoundTypeId.ChooseWeapon);
            _weaponIcons = provider.Get().PopupIcons;
            _selectedWeaponEffects = effectsProvider.ChoosedPopupWeaponEffects;
        }

        private void Awake()
        {
            DisableAllGunIcons();

            DisableAllWhiteFrames();

            AnimateMainWeaponText();
        }

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnFocusChanged;
            Application.focusChanged += OnFocusChanged;
            _adButton.onClick.AddListener(OnAdClicked);
            List<WeaponSelectorView> randomIcons = GetRandomWeaponIcons(3);
            EnableRandomIcons(randomIcons);
            _adButton.enabled = true;
            _hasFocus = Application.isFocused;
            _canvas.enabled = true;
            _adButton.image.DOFade(0, 0).SetUpdate(true);
            _canvasGroup.DOFade(1f, _canvasFadeDuration)
                .OnComplete(InitTimer).SetUpdate(true);
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnFocusChanged;
            WebApplication.InBackgroundChangeEvent -= OnFocusChanged;
            _adButton.onClick.RemoveListener(OnAdClicked);
            _canvasGroup.DOFade(0f, _canvasNonFadeDuration)
                .OnComplete(() =>
                {
                    _pauseService.UnPause();
                    _canvas.enabled = false;
                })
                .SetUpdate(true);
            
            StartChooseRandomWeapon().Forget();
        }

        private void OnFocusChanged(bool hasFocus)
        {
            _hasFocus = hasFocus;
        }

        private async void InitTimer()
        {
            _adButton.image.DOFade(1, 0.5f).SetUpdate(true);
            await _popupTimerService.Init();
        }

        private void AnimateMainWeaponText()
        {
            var targetColor = new Color(_color.r, _color.g, _color.b, _mainWeaponText.color.a);
            _mainWeaponText
                .DOColor(targetColor, 0.5f)
                .SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        }

        private void OnAdClicked()
        {
            _adButton.enabled = false;
            AdButtonClicked?.Invoke();
        }

        private void DisableAllWhiteFrames()
        {
            foreach (var whiteFrame in _whiteFrames.Values)
            {
                Color currentColor = whiteFrame.color;

                Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);

                whiteFrame.color = newColor;
            }
        }

        private void EnableRandomIcons(List<WeaponSelectorView> randomIcons)
        {
            foreach (var icon in randomIcons)
            {
                icon.gameObject.SetActive(true);
            }
        }

        private void DisableAllGunIcons()
        {
            foreach (var weaponSelector in _weaponIcons.Values)
            {
                weaponSelector.gameObject.SetActive(false);
            }
        }

        public async UniTask StartChooseRandomWeapon()
        {
            for (int i = 0; i < _chooseCycle; i++)
            {
                while (_adService.IsAdEnabled || !_hasFocus)
                {
                    await UniTask.Yield();
                }
                
                if (_lastWeaponSelected || !gameObject.activeSelf)
                    break;

                WeaponSelectorView currentWeaponView = await GetRandomWeaponView();

                if (_lastWeaponSelectorView is not null &&
                    _lastWeaponSelectorView.WeaponTypeId == currentWeaponView.WeaponTypeId)
                {
                    currentWeaponView = await GetRandomWeaponView();
                }

                _lastWeaponSelectorView = currentWeaponView;

                if (i == _chooseCycle - 1)
                {
                    _lastWeaponSelectedSound.Play();

                    var randomParticleId = Random.Range(0, _selectedWeaponEffects.Count - 1);
                    ParticleSystem targetParticle = _selectedWeaponEffects[randomParticleId];

                    targetParticle.transform.position = _lastWeaponSelectorView.transform.position;
                    targetParticle.Play();
                    LastWeaponSelected?.Invoke(_lastWeaponSelectorView.WeaponTypeId);
                    _lastWeaponSelected = true;
                }

                _chooseWeaponSound.Play();

                await UniTask.WaitForSeconds(_chooseDuration, true);
            }
        }

        private async UniTask<WeaponSelectorView> GetRandomWeaponView()
        {
            var lastFrameChanged = false;

            if (_lastFrame is not null)
            {
                Color currentCollor = new Color(_lastFrame.color.r, _lastFrame.color.g, _lastFrame.color.b, 0f);
                _lastFrame.DOColor(currentCollor, _closeWhiteFrameDuration).OnComplete(() => lastFrameChanged = true)
                    .SetUpdate(true);

                while (lastFrameChanged)
                {
                    await UniTask.Yield();
                }

                lastFrameChanged = false;
            }

            int randomWeaponId = Random.Range(0, _randomIcons.Count);

            WeaponSelectorView weaponSelectorView = _randomIcons[randomWeaponId];

            SetFrameAlpha(weaponSelectorView);

            return weaponSelectorView;
        }

        private void SetFrameAlpha(WeaponSelectorView weaponSelectorView)
        {
            var whiteFrame = _whiteFrames[weaponSelectorView.WeaponTypeId];

            Color currentColor = whiteFrame.color;

            var newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
            whiteFrame.DOColor(newColor, _openWhiteFrameDuration).SetUpdate(true);
            _lastFrame = whiteFrame;
        }

        private List<WeaponSelectorView> GetRandomWeaponIcons(int count)
        {
            List<WeaponSelectorView> allIcons = new List<WeaponSelectorView>(_weaponIcons.Values);
            int totalIcons = allIcons.Count;

            for (int i = 0; i < count; i++)
            {
                if (totalIcons == 0)
                    break;

                int randomIndex = Random.Range(0, totalIcons);
                _randomIcons.Add(allIcons[randomIndex]);
                allIcons.RemoveAt(randomIndex);
                totalIcons--;
            }

            return _randomIcons;
        }
    }
}