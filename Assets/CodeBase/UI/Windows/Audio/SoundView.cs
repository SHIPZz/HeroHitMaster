using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.UI.Windows.Audio
{
    public class SoundView : MonoBehaviour
    {
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public SoundTypeId SoundTypeId { get; private set; }
    }
}