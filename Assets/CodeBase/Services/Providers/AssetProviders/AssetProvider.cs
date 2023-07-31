using UnityEngine;

namespace CodeBase.Services.Providers.AssetProviders
{
    public class AssetProvider
    {
        public T GetAsset<T>(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return prefab.GetComponent<T>();
        }
        
        public GameObject GetAsset(string path) => 
            Resources.Load<GameObject>(path);
    }
}
