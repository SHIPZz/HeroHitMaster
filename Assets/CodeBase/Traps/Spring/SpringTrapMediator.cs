using CodeBase.Gameplay.PhysicalButtons;
using UnityEngine;

namespace CodeBase.Traps.Spring
{
    public class SpringTrapMediator : MonoBehaviour
    {
        [SerializeField] private SpringTrap _springTrap;
        [SerializeField] private PhysicalButton _physicalButton;

        private void OnEnable() => 
            _physicalButton.Pressed += _springTrap.Spring;

        private void OnDisable() => 
            _physicalButton.Pressed -= _springTrap.Spring;
    }
}