using UnityEngine;

namespace Gameplay.Web
{
    public interface IWeapon
    {
        int Id { get; }
        GameObject GameObject { get; }
        void Shoot(Vector3 target, Vector3 initialPosition, Web web);
    }
}