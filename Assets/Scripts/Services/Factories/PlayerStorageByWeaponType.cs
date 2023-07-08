using System;
using System.Collections.Generic;
using Enums;
using Services.GameObjectsPoolAccess;
using Services.Providers;
using Services.Providers.AssetProviders;

namespace Services.Factories
{
    public class PlayerStorageByWeaponType
    {
        private readonly PlayerStorage _playerStorage;
        private readonly AssetProvider _assetProvider;
        private Dictionary<WeaponTypeId, PlayerTypeId> _characters;
        private PlayerFactory _playerFactory;
        private LocationProvider _locationProvider;

        public PlayerStorageByWeaponType()
        {
            _characters = new Dictionary<WeaponTypeId, PlayerTypeId>()
            {
                { WeaponTypeId.SharpWebShooter, PlayerTypeId.Spider },
                { WeaponTypeId.SmudgeWebShooter, PlayerTypeId.Spider },
                { WeaponTypeId.WebSpiderShooter, PlayerTypeId.Spider },
                { WeaponTypeId.FireBallShooter, PlayerTypeId.Wolverine }
            };
        }

        public PlayerTypeId Get(WeaponTypeId weaponTypeId)
        {
            if (!_characters.TryGetValue(weaponTypeId, out var playerTypeId))
            {
                throw new ArgumentException("Error");
            }

            return playerTypeId;
        }
    }
}