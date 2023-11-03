using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.ScriptableObjects.Sound
{
    [CreateAssetMenu(menuName = "Gameplay/Sound Data", fileName = "SoundData")]
    public class SoundData : ScriptableObject
    {
        public SoundTypeId SoundTypeId;
        public AudioSource AudioSource;
        public MusicTypeId MusicTypeId;
    }
}