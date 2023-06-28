using Enums;
using UnityEngine;

namespace Gameplay.Web
{
    public interface IWeapon
    {
        WeaponTypeId WeaponTypeId { get; }
        GameObject GameObject { get; }
        void Shoot(Vector3 target, Vector3 initialPosition);
    }
}