using System.Collections.Generic;
using Constants;
using Enums;
using Gameplay.Weapons;
using Services.Providers.AssetProviders;
using UnityEngine;
using Zenject;

namespace Services.Factories
{
    public class WeaponFactory 
    {
        private readonly AssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private Dictionary<WeaponTypeId, string> _weapons;

        public WeaponFactory(AssetProvider assetProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
            
            FillDictionary();
        }

        public Weapon Create(WeaponTypeId weaponTypeId, Transform parent)
        {
            if (!_weapons.TryGetValue(weaponTypeId, out var prefabPath))
            {
                Debug.Log("ERROR");
                return null;
            }

            return Create(prefabPath, parent);
        }

        private void FillDictionary()
        {
            _weapons = new Dictionary<WeaponTypeId, string>()
            {
                { WeaponTypeId.WebSpiderShooter, AssetPath.SpiderWebShooter },
                { WeaponTypeId.SmudgeWebShooter, AssetPath.SmudgeWebShooter },
                { WeaponTypeId.FireBallShooter, AssetPath.FireBallRightHand },
                { WeaponTypeId.SharpWebShooter, AssetPath.SharpWebShooter },
                { WeaponTypeId.ThrowingKnifeShooter, AssetPath.ThrowingKnifeShooter },
                { WeaponTypeId.ThrowingHammerShooter, AssetPath.ThrowingHammerShooter },
                { WeaponTypeId.ThrowingTridentShooter, AssetPath.ThrowingTridentShooter },
                { WeaponTypeId.ThrowingIceCreamShooter, AssetPath.ThrowingIceCreamShooter },
            };
        }

        private Weapon Create(string prefabGunPath, Transform parent)
        {
            var gunPrefab = _assetProvider.GetAsset<Weapon>(prefabGunPath);
            return _diContainer.InstantiatePrefabForComponent<Weapon>(gunPrefab);
        }
    }
}