using Enums;

namespace Gameplay.Weapon
{
    public class RightHandShooter : Weapon
    {
        public override void Initialize()
        {
            WeaponTypeId = WeaponTypeId.FireBallShooter;
        }
    }
}