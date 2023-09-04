using System.Collections.Generic;
using CodeBase.Gameplay.ExplosionBarrel;
using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class ExplosionBarrelsProvider : MonoBehaviour, IProvider<List<ExplosionBarrel>>
    {
        [SerializeField] private List<ExplosionBarrel> _explosionBarrels;
        
        public List<ExplosionBarrel> Get()
        {
            var barrels = new List<ExplosionBarrel>();

            foreach (var explosionBarrel in _explosionBarrels)
            {
                barrels.Add(explosionBarrel);
            }
            
            return barrels;
        }

        public void Set(List<ExplosionBarrel> t)
        {
        }
    }
}