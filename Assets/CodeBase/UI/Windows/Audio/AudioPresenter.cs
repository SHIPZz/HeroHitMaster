using System;
using Zenject;

namespace CodeBase.UI.Windows.Audio
{
    public class AudioPresenter : IInitializable, IDisposable
    {
        private readonly AudioView _audioView;
        private readonly AudioChanger _audioChanger;

        public AudioPresenter(AudioView audioView, AudioChanger audioChanger)
        {
            _audioView = audioView;
            _audioChanger = audioChanger;
        }

        public void Initialize() => 
            _audioView.ValueChanged += _audioChanger.Change;

        public void Dispose() => 
            _audioView.ValueChanged -= _audioChanger.Change;
    }
}