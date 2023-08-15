using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.ScriptableObjects.Weapon
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Gameplay/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [Range(0.3f, 1)] public float ShootDelay;

        [Range(0,4000)] public int Price;
    
        public WeaponTypeId WeaponTypeId;
        public ParticleSystem ShootEffect;
        public AudioSource ShootSound;
        public Gameplay.Weapons.Weapon Prefab;
        public WeaponRankId WeaponRankId;
    }
}
