using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace CodeBase.Services.Providers.AssetProviders
{
    public class AssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle>  _completedCaches = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>>  _handles = new();
        
        public void Initialize() => 
            Addressables.InitializeAsync();

        public GameObject GetAsset(string path) => 
            Resources.Load<GameObject>(path);

        public async UniTask<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_completedCaches.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(assetReference), assetReference.AssetGUID);
        }

        public async UniTask<T> Load<T>(string address) where T : class
        {
            if (_completedCaches.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(address), address);
        }

        public T GetAsset<T>(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return prefab.GetComponent<T>();
        }

        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> resourceHandler in _handles.Values)
            {
                foreach(AsyncOperationHandle handle in resourceHandler)
                    Addressables.Release(handle);
            }
            
            _completedCaches.Clear();
            _handles.Clear();
        }

        private async UniTask<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle => _completedCaches[cacheKey] = handle;

            AddHandle(cacheKey, handle);

            return await handle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandle))
            {
                resourceHandle = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandle;
            }

            resourceHandle.Add(handle);
        }
    }
}
