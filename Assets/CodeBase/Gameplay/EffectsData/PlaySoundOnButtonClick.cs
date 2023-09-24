using CodeBase.Enums;
using CodeBase.Services.Storages.Sound;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Gameplay.EffectsData
{
    public class PlaySoundOnButtonClick : MonoBehaviour
    {
        [SerializeField] private SoundTypeId _soundTypeId;
    
        private Button _button;
        private AudioSource _targetSound;

        [Inject]
        private void Construct(ISoundStorage soundStorage) => 
            _targetSound = soundStorage.Get(_soundTypeId);

        private void Awake() => 
            _button = GetComponent<Button>();

        private void OnEnable() => 
            _button.onClick.AddListener(Play);

        private void OnDisable() => 
            _button.onClick.RemoveListener(Play);

        private void Play() => 
            _targetSound.Play();
    }
}
