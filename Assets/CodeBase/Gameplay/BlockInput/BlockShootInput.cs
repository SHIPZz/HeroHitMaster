using CodeBase.Services.Inputs.InputService;

namespace CodeBase.Gameplay.BlockInput
{
    public class BlockShootInput
    {
        private readonly IInputService _inputService;

        public BlockShootInput(IInputService inputService) => 
            _inputService = inputService;

        public void EnableInput() => 
            _inputService.PlayerFire.Enable();

        public void DisableInput() => 
            _inputService.PlayerFire.Disable();
    }
}