using System.Collections;
using System.Globalization;
using CodeBase.Enums;
using CodeBase.Services.Pause;
using CodeBase.Services.Storages.Sound;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows.Popup
{
    public class DisablePopupTimer : MonoBehaviour
    {
        private const float FadeDuration = 1f;
        private const float CountdownInterval = 0.5f;
        private const float FinishDelay = 0.5f;
        
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Image _whiteFrame;
        [SerializeField] private Window _playWindow;
        [SerializeField] private float _startTime = 3f;
        [SerializeField] private float _startDelay = 2f;

        private AudioSource _timerTicking;
        private AudioSource _timerFinished;
        private PopupInfoView _popupInfoView;
        private IPauseService _pauseService;

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

        private void OnEnable()
        {
            _timerText.text = _startTime.ToString(CultureInfo.InvariantCulture);
            _popupInfoView.AdButtonClicked += StopTimer;
            StartCountdown();
        }

        private void OnDisable()
        {
            _popupInfoView.AdButtonClicked -= StopTimer;
            _pauseService.UnPause();
            PauseCoroutines();
        }

        private void StopTimer()
        {
            PauseCoroutines();
            _timerTicking.Stop();
            _timerFinished.Stop();
            gameObject.SetActive(false);
        }

        private void StartCountdown() => 
            StartCoroutine(CountdownCoroutine());

        private IEnumerator CountdownCoroutine()
        {
            _timerText.text = _startTime.ToString(CultureInfo.InvariantCulture);
            _pauseService.Pause();
            yield return new WaitForSecondsRealtime(_startDelay);

            _timerTicking.Play();

            while (_startTime != 0)
            {
                FadeInOut(_whiteFrame, _timerText, FadeDuration, true);
                yield return new WaitForSecondsRealtime(CountdownInterval);
                _startTime--;
                _timerText.text = _startTime.ToString(CultureInfo.InvariantCulture);
                yield return new WaitForSecondsRealtime(CountdownInterval);
                FadeInOut(_whiteFrame, _timerText, FadeDuration, false);

                if (_startTime == 0)
                {
                    _timerTicking.Stop();
                    _timerText.text = _startTime.ToString(CultureInfo.InvariantCulture);
                    yield return new WaitForSecondsRealtime(FinishDelay);
                    _timerFinished.Play();
                    _playWindow.gameObject.SetActive(true);
                    _playWindow.transform.localScale = Vector3.one;
                    _popupInfoView.gameObject.SetActive(false);
                    _pauseService.UnPause();
                }

                yield return new WaitForSecondsRealtime(CountdownInterval);
            }
        }

        private void PauseCoroutines() => 
            StopCoroutine(CountdownCoroutine());

        private void FadeInOut(Image image, TextMeshProUGUI text, float duration, bool fadeIn)
        {
            image.DOFade(fadeIn ? 1 : 0, duration).SetUpdate(true);
            text.DOFade(fadeIn ? 1 : 0, duration).SetUpdate(true);
        }
    }
}
