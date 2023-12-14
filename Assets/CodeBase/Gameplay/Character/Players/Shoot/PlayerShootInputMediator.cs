using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Infrastructure;
using CodeBase.Services.Providers;
using CodeBase.Services.Storages.Character;
using CodeBase.UI.Windows;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Character.Players.Shoot
{
    public class PlayerShootInputMediator : IInitializable, IDisposable, IGameplayRunnable
    {
        private readonly List<Player> _players;
        private readonly Window _pauseWindow;
        private readonly Window _hudWindow;

        public PlayerShootInputMediator(IPlayerStorage playerStorage, WindowProvider windowProvider)
        {
            _pauseWindow = windowProvider.Windows[WindowTypeId.Pause];
            _hudWindow = windowProvider.Windows[WindowTypeId.Hud];
            _players = playerStorage.GetAll();
        }

        public void Run() =>
            _players.ForEach(x => x.GetComponent<PlayerShootInput>().UnBlock());

        public void Initialize()
        {
            _pauseWindow.StartedToOpen += BlockInput;
            _hudWindow.Opened += UnblockInput;
        }

        public void Dispose()
        {
            _pauseWindow.StartedToOpen -= BlockInput;
            _hudWindow.Opened -= UnblockInput;
        }

        private void UnblockInput() => 
            _players.ForEach(x => x.GetComponent<PlayerShootInput>().UnBlock());

        private void BlockInput() => 
            _players.ForEach(x => x.GetComponent<PlayerShootInput>().Block());
    }
}