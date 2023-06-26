using Enums;
using UnityEngine;

namespace ScriptableObjects.WebSettings
{
    [CreateAssetMenu(fileName = "WebSettings", menuName = "Gameplay/WebSettings")]
    public class WebSettings : ScriptableObject
    {
        [field: SerializeField] public WebTypeId WebTypeId { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}