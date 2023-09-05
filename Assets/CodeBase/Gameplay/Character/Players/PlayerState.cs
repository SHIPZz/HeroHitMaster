using System;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Providers;
using UnityEngine.AI;

namespace CodeBase.Gameplay.Character.Players
{
    public class PlayerState : IDisposable
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly PlayerAnimator _playerAnimator;
        private Weapon _weapon;

        public PlayerState(NavMeshAgent navMeshAgent, IProvider<WeaponProvider> weaponProvider,
            PlayerAnimator playerAnimator)
        {
            _playerAnimator = playerAnimator;
            weaponProvider.Get().Changed += SetWeapon;
            _navMeshAgent = navMeshAgent;
        }

        private void SetWeapon(Weapon obj)
        {
            _weapon = obj;
            _weapon.Shooted += OnWeaponShooted;
        }

        private void OnWeaponShooted()
        {
            // _playerAnimator.NeedBlockIdle(_navMeshAgent.velocity.magnitude > 0.1);
            //
            // if (_navMeshAgent.velocity.magnitude > 0.1f)
            //     _playerAnimator.SetMovement(1f);
        }

        public void Dispose()
        {
            _weapon.Shooted -= OnWeaponShooted;
        }
    }
}