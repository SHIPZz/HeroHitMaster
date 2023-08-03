using CodeBase.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Gameplay/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Range(0.3f, 1)] public float ShootDelay;

    [Range(0,4000)] public int Price;
    
    public WeaponTypeId WeaponTypeId;

}
