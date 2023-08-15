using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI.Weapons.ShopWeapons
{
    public class ShopWeaponIconMoveOnEnable : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.DOMoveY(Vector3.up.y * 10, 1f).SetLoops(-1);
        }
    }
}