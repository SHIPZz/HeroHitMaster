using System.Collections.Generic;
using ScriptableObjects.PlayerSettings;
using Services.Providers;
using Zenject;

namespace Services.Factories
{
    public class UIFactory
    {
        private readonly DiContainer _diContainer;
        private readonly List<PlayerSettings> _playerSettingsList;
        private readonly LocationProvider _locationProvider;
        private readonly WeaponProvider _weaponProvider;

        public UIFactory(DiContainer diContainer, List<PlayerSettings> playerSettingsList,
            LocationProvider locationProvider,
            WeaponProvider weaponProvider)
        {
            _diContainer = diContainer;
            _playerSettingsList = playerSettingsList;
            _locationProvider = locationProvider;
            _weaponProvider = weaponProvider;
        }
    }
}