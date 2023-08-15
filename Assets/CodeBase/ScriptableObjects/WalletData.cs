using UnityEngine;

[CreateAssetMenu(fileName = "WalletData", menuName = "Gameplay/Wallet Data")]
public class WalletData : ScriptableObject
{
    [Range(3000, 10000)] public int MaxMoney = 5000;
}
