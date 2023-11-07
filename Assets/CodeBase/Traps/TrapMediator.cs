using CodeBase.Gameplay.PhysicalButtons;
using UnityEngine;

namespace CodeBase.Traps
{
    public class TrapMediator : MonoBehaviour
    {
        [SerializeField] private Trap _trap;
        [SerializeField] private PhysicalButton _physicalButton;

        private void OnEnable()
        {
            _physicalButton.SetEnablingDelay(_trap.DisableDelay);
            _physicalButton.Pressed += _trap.Activate;
        }

        private void OnDisable() => 
            _physicalButton.Pressed -= _trap.Activate;
    }
}