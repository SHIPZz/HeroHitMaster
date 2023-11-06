using System;
using CodeBase.Gameplay.Character.Players.Shoot;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Providers;
using UnityEngine.AI;

namespace CodeBase.Gameplay.Character.Players
{
    public class PlayerState : IDisposable
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly PlayerAnimator _playerAnimator;
        private readonly ShootingOnAnimationEvent _shootingOnAnimationEvent;
        private Weapon _weapon;
        private readonly WeaponProvider _weaponProvider;

        public PlayerState(NavMeshAgent navMeshAgent, IProvider<WeaponProvider> weaponProvider,
            PlayerAnimator playerAnimator, ShootingOnAnimationEvent shootingOnAnimationEvent)
        {
            _shootingOnAnimationEvent = shootingOnAnimationEvent;
            _playerAnimator = playerAnimator;
            _weaponProvider = weaponProvider.Get();
            _navMeshAgent = navMeshAgent;
            _weaponProvider.Changed += SetWeapon;
            _shootingOnAnimationEvent.Stopped += OnWeaponStoppedShoot;
        }

        public void Dispose()
        {
            _weapon.Shot -= OnWeaponShot;
            _weaponProvider.Changed -= SetWeapon;
            _shootingOnAnimationEvent.Stopped -= OnWeaponStoppedShoot;
        }

        private void OnWeaponStoppedShoot()
        {
            if (_navMeshAgent.velocity.magnitude > 0.1f)
            {
                _playerAnimator.SetMovement(1f);
            }
        }

        private void SetWeapon(Weapon obj)
        {
            _weapon = obj;
            _weapon.Shot += OnWeaponShot;
        }

        private void OnWeaponShot() => 
            _playerAnimator.NeedBlockIdle(_navMeshAgent.velocity.magnitude > 0.1);
    }
}