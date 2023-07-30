using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Character.Players;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Factories;
using Enums;
using UnityEngine;

namespace Services.Factories
{
    public class GameFactory
    {
        private readonly PlayerFactory _playerFactory;
        private readonly PlayerCameraFactory _playerCameraFactory;
        private readonly WeaponFactory _weaponFactory;

        public GameFactory(PlayerFactory playerFactory,PlayerCameraFactory playerCameraFactory,
            WeaponFactory weaponFactory)
        {
            _playerFactory = playerFactory;
            _playerCameraFactory = playerCameraFactory;
            _weaponFactory = weaponFactory;
        }

        public PlayerCameraFollower CreateCamera(Vector3 at) =>
            _playerCameraFactory.Create(at);
        
        public Player CreatePlayer(PlayerTypeId playerTypeId, Vector3 at) =>
            _playerFactory.Create(playerTypeId, at);

        public Weapon CreateWeapon(WeaponTypeId weaponTypeId) =>
            _weaponFactory.Create(weaponTypeId);
    }
}