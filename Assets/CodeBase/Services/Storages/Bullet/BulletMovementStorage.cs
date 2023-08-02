using System.Collections.Generic;
using CodeBase.Enums;
using CodeBase.Gameplay.Bullet;
using CodeBase.Gameplay.Bullet.Web;
using CodeBase.Services.Data;

namespace CodeBase.Services.Storages.Bullet
{
    public class BulletMovementStorage
    {
        private Dictionary<WeaponTypeId, IBulletMovement> _bulletMovements;

        public BulletMovementStorage(BulletStaticDataService bulletStaticDataService) => 
            FillDictionary(bulletStaticDataService);

        public IBulletMovement GetBulletMovementBy(WeaponTypeId weaponTypeId) =>
            _bulletMovements[weaponTypeId];

        private void FillDictionary(BulletStaticDataService bulletStaticDataService)
        {
            _bulletMovements = new Dictionary<WeaponTypeId, IBulletMovement>
            {
                { WeaponTypeId.FireBallShooter, new DefaultBulletMovement() },
                { WeaponTypeId.SuperWeapon, new DefaultBulletMovement() },
                { WeaponTypeId.WebSpiderShooter, new WebMovement() },
                { WeaponTypeId.SharpWebShooter, new WebMovement() },
                { WeaponTypeId.SmudgeWebShooter, new WebMovement() },
                { WeaponTypeId.ThrowingDynamiteShooter, new KnifeMovement(bulletStaticDataService) },
                { WeaponTypeId.ThrowingHammerShooter, new KnifeMovement(bulletStaticDataService) },
                { WeaponTypeId.ThrowingKnifeShooter, new KnifeMovement(bulletStaticDataService) },
                { WeaponTypeId.ThrowingTridentShooter, new KnifeMovement(bulletStaticDataService) },
                { WeaponTypeId.ThrowingIceCreamShooter, new KnifeMovement(bulletStaticDataService) },
            };
        }
    }
}