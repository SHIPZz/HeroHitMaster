using UnityEngine;

namespace Services.Providers.AssetProviders
{
    public class AssetProvider
    {
        public T GetAsset<T>(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return prefab.GetComponent<T>();
        }
        
        public GameObject GetAsset(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return prefab;
        }
    }
}
