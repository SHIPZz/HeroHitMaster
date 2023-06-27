using Constants;
using Databases;
using Enums;
using Gameplay.Web;
using UnityEngine;

namespace Gameplay.Character.Player.Shoot
{
    public class ShootHand : MonoBehaviour, IWeapon
    {
        public Web.Web Web { get; private set; }
        
        private readonly WebMovement _webMovement;
        private readonly WeaponsProvider _weaponsProvider;

        public ShootHand(WebMovement webMovement,WeaponsProvider weaponsProvider)
        {
            _webMovement = webMovement;
            _weaponsProvider = weaponsProvider;
            Id = WeaponId.ShootHand;
            GameObject = gameObject;
            _weaponsProvider.Add(this);
        }

        public int Id { get; }
        public GameObject GameObject { get; }

        public void Shoot(Vector3 target, Vector3 initialPosition, Web.Web web)
        {
            _webMovement.Move(target, web, initialPosition);
        }
    }
}