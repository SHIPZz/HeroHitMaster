using Enums;
using Gameplay.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Gameplay/WeaponSettings",fileName = "WeaponSettings")]
    public class WeaponSettings : ScriptableObject
    {
        [SerializeField] private Image _imagePrefab;
        [SerializeField] private WeaponTypeId _weaponTypeId;
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private int _price;
        [SerializeField] private Animator _animator;

        public Weapon WeaponPrefab => _weaponPrefab;

        public WeaponTypeId WeaponTypeId => _weaponTypeId;

        public Animator Animator => _animator;

        public int Price => _price;

        public Image ImagePrefab => _imagePrefab;
    }
}