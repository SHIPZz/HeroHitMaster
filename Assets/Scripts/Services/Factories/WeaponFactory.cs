using Gameplay.Web;
using Services.ObjectPool;
using Services.Providers.AssetProviders;
using UnityEngine;

namespace Services.Factories
{
    public class WeaponFactory
    {
        private const int Count = 50;
        
       private readonly AssetProvider _assetProvider;

       public WeaponFactory(AssetProvider assetProvider)
       {
           _assetProvider = assetProvider;
       }

        public IWeapon Get(string name)
        {
            GameObject weaponPrefab = _assetProvider.GetAsset(name);
            var gameObjectPool = new GameObjectPool(() => weaponPrefab, Count);
            return weaponPrefab.GetComponent<IWeapon>();
        }
    }
}