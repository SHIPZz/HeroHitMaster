using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.ScriptableObjects.Bullet
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Gameplay/BulletData")]
    public class BulletData : ScriptableObject
    {
        [Range(0.1f, 1)] public float MovementDuration;
        [Range(0.1f, 1)] public float RotateDuration = 0.3f;
        
        [Range(20, 1000)] public int Damage;
        
        [Range(30, 100)] public int Count;
        
        public BulletTypeId BulletTypeId;
    }
}