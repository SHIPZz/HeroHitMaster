using Enums;
using UnityEngine;

namespace ScriptableObjects.WebSettings
{
    [CreateAssetMenu(fileName = "WebSettings", menuName = "Gameplay/WebSettings")]
    public abstract class WebSettings : ScriptableObject
    {
        [field: SerializeField] public WebTypeId WebTypeId { get; protected set; }
        [field: SerializeField] public float Speed { get; protected set; }
        [field: SerializeField] public int Damage { get; protected set; }
    }
}