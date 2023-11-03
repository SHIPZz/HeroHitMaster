using System;
using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Audio
{
    public class AudioPresenter : IInitializable, IDisposable
    {
        private readonly AudioVolumeChanger _audioVolumeChanger;
        private readonly List<AudioSliderView> _audioSliderViews;
        private readonly IWorldDataService _worldDataService;

        public AudioPresenter(List<AudioSliderView> audioSliderViews,
            AudioVolumeChanger audioVolumeChanger, 
            IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
            _audioSliderViews = audioSliderViews;
            _audioVolumeChanger = audioVolumeChanger;
        }

        public void Initialize()
        {
            foreach (AudioSliderView audioSliderView in _audioSliderViews)
            {
                if (audioSliderView.AudioViewTypeId == AudioViewTypeId.Music)
                {
                    audioSliderView.ValueChanged += _audioVolumeChanger.ChangeMusic;
                    continue;
                }

                audioSliderView.ValueChanged += _audioVolumeChanger.Change;
            }

            SetInitialDataToView(_worldDataService.WorldData.SettingsData.Volume, 
            _worldDataService.WorldData.SettingsData.MusicVolume);
        }

        public void Dispose()
        {
            foreach (AudioSliderView audioSliderView in _audioSliderViews)
            {
                if (audioSliderView.AudioViewTypeId == AudioViewTypeId.Music)
                {
                    audioSliderView.ValueChanged -= _audioVolumeChanger.ChangeMusic;
                    continue;
                }

                audioSliderView.ValueChanged -= _audioVolumeChanger.Change;
            }
        }

        private void SetInitialDataToView(float soundsValue, float musicValue)
        {
            foreach (AudioSliderView audioSliderView in _audioSliderViews)
            {
                if (audioSliderView.AudioViewTypeId == AudioViewTypeId.AllSounds)
                {
                    audioSliderView.Slider.value = soundsValue;
                    continue;
                }

                audioSliderView.Slider.value = musicValue;
            }
        }
    }
}