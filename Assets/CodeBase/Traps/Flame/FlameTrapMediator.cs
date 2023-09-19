using CodeBase.Gameplay.PhysicalButtons;
using UnityEngine;

namespace CodeBase.Traps.Flame
{
    public class FlameTrapMediator : MonoBehaviour
    {
        [SerializeField] private FlameTrap _flameTrap;
        [SerializeField] private PhysicalButton _physicalButton;

        private void OnEnable() => 
            _physicalButton.Pressed += _flameTrap.Fire;

        private void OnDisable() => 
            _physicalButton.Pressed -= _flameTrap.Fire;
    }
}