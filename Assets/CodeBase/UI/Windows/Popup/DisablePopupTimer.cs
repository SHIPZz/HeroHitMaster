using System;
using System.Collections;
using System.Globalization;
using CodeBase.Enums;
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
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Image _whiteFrame;
        [SerializeField] private Window _playWindow;
        [SerializeField] private float _startTime = 3f;
        [SerializeField] private float _startDelay = 2f;

        private AudioSource _timerTicking;
        private AudioSource _timerFinished;
        private PopupInfoView _popupInfoView;

        [Inject]
        private void Construct(ISoundStorage soundStorage, PopupInfoView popupInfoView)
        {
            _popupInfoView = popupInfoView;
            _timerTicking = soundStorage.Get(SoundTypeId.TimerTicking);
            _timerFinished = soundStorage.Get(SoundTypeId.TimerFinished);
        }

        private void Awake()
        {
            _whiteFrame.DOFade(0, 0f);
            _timerText.DOFade(0, 0f);
        }

        private void OnEnable()
        {
            _timerText.text = _startTime.ToString(CultureInfo.InvariantCulture);
            _popupInfoView.AdButtonClicked += StopTimer;
            StartCoroutine(nameof(DecreaseStartTimeCoroutine));
        }

        private void OnDisable() => 
            _popupInfoView.AdButtonClicked -= StopTimer;

        private void StopTimer()
        {
            StopCoroutine(DecreaseStartTimeCoroutine());
            _timerTicking.Stop();
            _timerFinished.Stop();
            gameObject.SetActive(false);
        }

        private IEnumerator DecreaseStartTimeCoroutine()
        {
            yield return new WaitForSeconds(_startDelay);
            
            _timerTicking.Play();

            while (_startTime != 0)
            {
                _whiteFrame.DOFade(1, 1f);
                _timerText.DOFade(1, 1f);
                yield return new WaitForSeconds(0.5f);
                _startTime--;
                _timerText.text = _startTime.ToString(CultureInfo.InvariantCulture);
                yield return new WaitForSeconds(1f);
                _whiteFrame.DOFade(0, 1f);
                _timerText.DOFade(0, 1f);

                if (_startTime == 0)
                {
                    _timerTicking.Stop();
                    _timerText.text = _startTime.ToString(CultureInfo.InvariantCulture);
                    yield return new WaitForSeconds(0.5f);
                    _timerFinished.Play();
                    _playWindow.gameObject.SetActive(true);
                    _playWindow.transform.localScale = Vector3.one;
                    _popupInfoView.gameObject.SetActive(false);
                }

                yield return new WaitForSeconds(1f);
            }
        }
    }
}