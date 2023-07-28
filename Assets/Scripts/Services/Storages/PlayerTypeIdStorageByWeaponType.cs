using System;
using System.Collections.Generic;
using Enums;
using Services.Factories;
using Services.Providers;
using Services.Providers.AssetProviders;

namespace Services.Storages
{
    public class PlayerTypeIdStorageByWeaponType 
    {
        private readonly Dictionary<WeaponTypeId, PlayerTypeId> _characters = new()
        {
            { WeaponTypeId.SharpWebShooter, PlayerTypeId.Spider },
            { WeaponTypeId.SmudgeWebShooter, PlayerTypeId.Spider },
            { WeaponTypeId.WebSpiderShooter, PlayerTypeId.Spider },
            { WeaponTypeId.FireBallShooter, PlayerTypeId.Wizard },
            { WeaponTypeId.ThrowingKnifeShooter, PlayerTypeId.Wolverine },
            { WeaponTypeId.ThrowingHammerShooter, PlayerTypeId.Wolverine },
            { WeaponTypeId.ThrowingTridentShooter, PlayerTypeId.Wolverine },
            { WeaponTypeId.ThrowingIceCreamShooter, PlayerTypeId.Wolverine },
        };

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