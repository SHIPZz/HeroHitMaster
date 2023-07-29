using Enums;
using UnityEngine;

namespace Services
{
    public class SoundView : MonoBehaviour
    {
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public SoundTypeId SoundTypeId { get; private set; }
    }
}