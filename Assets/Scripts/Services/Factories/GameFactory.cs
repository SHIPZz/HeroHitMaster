using Enums;
using Gameplay.Camera;
using Gameplay.Character.Player;
using Gameplay.Character.Player.Shoot;
using Gameplay.Web;
using UnityEngine;

namespace Services.Factories
{
    public class GameFactory
    {
        private readonly Player.Factory _playerFactory;
        private readonly PlayerCameraFollower.Factory _cameraFollowerFactory;
        private readonly WeaponFactory _weaponFactory;
        private readonly ShootHand.Factory _shootHandFactory;

        public GameFactory(Player.Factory playerFactory, PlayerCameraFollower.Factory cameraFollowerFactory,
            WeaponFactory weaponFactory)
        {
            _playerFactory = playerFactory;
            _cameraFollowerFactory = cameraFollowerFactory;
            _weaponFactory = weaponFactory;
        }

        public PlayerCameraFollower CreateCameraFollower(Player player) =>
            _cameraFollowerFactory.Create(player);

        public Player CreatePlayer(Vector3 at) => 
            _playerFactory.Create(at);

        public IWeapon CreateWeapon() =>
            _weaponFactory.Create();
    }
}