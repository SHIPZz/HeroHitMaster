using Enums;
using UnityEngine;

namespace ScriptableObjects.WebSettings
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "Gameplay/BulletSettings")]
    public class BulletSettings : ScriptableObject
    {
        [field: SerializeField] public BulletTypeId BulletTypeId { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}