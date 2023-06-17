using UnityEngine;

public class AssetProvider
{
    public GameObject GetAsset(string name)
    {
        GameObject prefab = Resources.Load<GameObject>(name);
        return prefab;
    }

    public T GetAsset<T>(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return prefab.GetComponent<T>();
    }
}
