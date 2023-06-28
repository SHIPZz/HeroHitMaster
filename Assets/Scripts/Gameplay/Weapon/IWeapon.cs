using Enums;
using UnityEngine;

namespace Gameplay.Weapon
{
    public interface IWeapon
    {
        WeaponTypeId WeaponTypeId { get; }
        GameObject GameObject { get; }
        void Shoot(Vector3 target, Vector3 initialPosition);
    }
}