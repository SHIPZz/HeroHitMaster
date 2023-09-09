using System;
using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.SaveTriggers;
using Zenject;

namespace CodeBase.Gameplay.Character.Players
{
    public class DisablePlayerOnLevelFinish : IInitializable, IDisposable
    {
        private readonly SaveTriggerOnLevelEnd _saveTriggerOnLevelEnd;
        private readonly Player _player;

        public DisablePlayerOnLevelFinish(SaveTriggerOnLevelEnd saveTriggerOnLevelEnd, Player player)
        {
            _saveTriggerOnLevelEnd = saveTriggerOnLevelEnd;
            _player = player;
        }

        public void Initialize() => 
        _saveTriggerOnLevelEnd.PlayerEntered += Disable;

        public void Dispose() => 
        _saveTriggerOnLevelEnd.PlayerEntered -= Disable;

        private void Disable() => 
            _player.gameObject.SetActive(false);
    }
}