using System;
using CodeBase.Gameplay.Character.Players;
using UnityEngine;

namespace CodeBase.Gameplay.ExplosionBarrel
{
    [RequireComponent(typeof(ExplosionBarrel))]
    public class ActivateBarrelOnPlayerEnter : MonoBehaviour
    {
        [SerializeField] private AggroZone _aggroZone;
        
        private ExplosionBarrel _explosionBarrel;

        private void Awake()
        {
            _explosionBarrel = GetComponent<ExplosionBarrel>();
            _explosionBarrel.GetComponent<Collider>().enabled = false;
        }

        private void OnEnable() => 
            _aggroZone.PlayerEntered += Activate;

        private void OnDisable() => 
        _aggroZone.PlayerEntered -= Activate;

        private void Activate(Player obj) => 
            _explosionBarrel.GetComponent<Collider>().enabled = true;
    }
}