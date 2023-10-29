﻿using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Gameover
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _gameFinishText;
        [SerializeField] private float _targetAlpha = 1f;
        [SerializeField] private float _showUpDuration = 1.5f;
        [SerializeField] private float _disableDelay = 5f;

        public event Action Disabled;
        
        public void Init()
        {
            _background.DOFade(_targetAlpha, _showUpDuration);
            _gameFinishText.DOFade(_targetAlpha, _showUpDuration);
            DOTween
                .Sequence()
                .AppendInterval(_disableDelay)
                .OnComplete(() => Disabled?.Invoke());
        }
    }
}