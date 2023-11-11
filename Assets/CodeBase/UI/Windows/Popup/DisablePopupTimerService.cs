using System;
using System.Globalization;
using System.Threading;
using CodeBase.Enums;
using CodeBase.Services.Pause;
using CodeBase.Services.Storages.Sound;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows.Popup
{
    public class DisablePopupTimerService : MonoBehaviour
    {
        private const float FadeDuration = 1f;
        private const float CountdownInterval = 0.5f;

        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Image _whiteFrame;
        [SerializeField] private float _startTime = 3f;
        [SerializeField] private float _startDelay = 2f;

        private AudioSource _timerTicking;
        private AudioSource _timerFinished;
        private PopupInfoView _popupInfoView;
        private IPauseService _pauseService;
        private bool _ignoreTimeScale;
        private bool _hasFocus;
        private bool _isDisabled;

        [Inject]
        private void Construct(ISoundStorage soundStorage, PopupInfoView popupInfoView, IPauseService pauseService)
        {
            _pauseService = pauseService;
            _popupInfoView = popupInfoView;
            _timerTicking = soundStorage.Get(SoundTypeId.TimerTicking);
            _timerFinished = soundStorage.Get(SoundTypeId.TimerFinished);
        }

        private void Awake() =>
            FadeInOut(_whiteFrame, _timerText, 0f, false);

        private async void OnEnable()
        {
            _timerText.text = _startTime.ToString(CultureInfo.InvariantCulture);
            _popupInfoView.AdButtonClicked += StopTimer;
            _hasFocus = Application.isFocused;
            _pauseService.Pause();
            Application.focusChanged += OnFocusChanged;

            await Init();
        }

        private void OnFocusChanged(bool hasFocus)
        {
            _hasFocus = hasFocus;
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnFocusChanged;
            _popupInfoView.AdButtonClicked -= StopTimer;
            _pauseService.UnPause();
        }

        private async UniTask Init()
        {
            _timerText.text = _startTime.ToString(CultureInfo.InvariantCulture);
            _pauseService.Pause();
            _timerTicking.Play();

            while (_startTime != 0 && !_isDisabled)
            {
                _pauseService.Pause();
                
                while (!_hasFocus)
                {
                    FadeInOut(_whiteFrame, _timerText, 0f, true);
                    await UniTask.Yield();
                }

                FadeInOut(_whiteFrame, _timerText, FadeDuration, true);
                await UniTask.Delay(TimeSpan.FromSeconds(CountdownInterval), true);
                _startTime--;
                _timerText.text = _startTime.ToString(CultureInfo.InvariantCulture);
                await UniTask.Delay(TimeSpan.FromSeconds(CountdownInterval), true);
                FadeInOut(_whiteFrame, _timerText, FadeDuration, false);

                if (_startTime == 0)
                {
                    _timerTicking.Stop();
                    _timerText.text = _startTime.ToString(CultureInfo.InvariantCulture);
                    await UniTask.Delay(TimeSpan.FromSeconds(CountdownInterval), true);
                    _timerFinished.Play();
                    _popupInfoView.gameObject.SetActive(false);
                    _pauseService.UnPause();
                }

                await UniTask.Delay(TimeSpan.FromSeconds(CountdownInterval), true);
            }
        }

        private void StopTimer()
        {
            Init().Forget();
            _timerTicking.Stop();
            _timerFinished.Stop();
            _isDisabled = true;
            gameObject.SetActive(false);
        }

        private void FadeInOut(Image image, TextMeshProUGUI text, float duration, bool fadeIn)
        {
            image.DOFade(fadeIn ? 1 : 0, duration).SetUpdate(true);
            text.DOFade(fadeIn ? 1 : 0, duration).SetUpdate(true);
        }
    }
}