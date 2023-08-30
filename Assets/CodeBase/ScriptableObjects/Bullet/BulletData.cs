using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.ScriptableObjects.Bullet
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Gameplay/BulletData")]
    public class BulletData : ScriptableObject
    {
        [Range(10f, 100)] public float Speed;
        [Range(10f, 100)] public float RotationSpeed;
        
        [Range(20, 1000)] public int Damage;
        
        [Range(30, 100)] public int Count;
        
        public WeaponTypeId WeaponTypeId;
        public GameObject Prefab;
        public ParticleSystem HitEffect;
        public ParticleSystem StartEffect;
    }
}