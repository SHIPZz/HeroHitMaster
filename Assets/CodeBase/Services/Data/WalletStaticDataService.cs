using CodeBase.ScriptableObjects;
using UnityEngine;

namespace CodeBase.Services.Data
{
    public class WalletStaticDataService
    {
        public WalletData Get() =>
            Resources.Load<WalletData>("Prefabs/WalletData/WalletData");
    }
}