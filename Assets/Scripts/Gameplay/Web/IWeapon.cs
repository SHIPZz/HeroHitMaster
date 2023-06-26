using UnityEngine;

namespace Gameplay.Web
{
    public interface IWeapon
    {
        void Shoot(Vector3 target, Vector3 initialPosition);
    }
}