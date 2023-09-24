using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Enums;
using CodeBase.Services.Providers;
using CodeBase.UI.Windows;
using Sirenix.Utilities;
using Zenject;

namespace CodeBase.Gameplay.BlockInput
{
    public class BlockShootInputPresenter : IInitializable, IDisposable
    {
        private readonly BlockShootInput _blockShootInput;
        private readonly List<Window> _allWindows;
        private readonly Window _playWindow;

        public BlockShootInputPresenter(WindowProvider windowProvider, BlockShootInput blockShootInput)
        {
            _blockShootInput = blockShootInput;
            _allWindows = windowProvider.Windows.Values.ToList();
            _playWindow = windowProvider.Windows[WindowTypeId.Play];
        }

        public void Initialize()
        {
            _allWindows.ForEach(x =>
            {
                x.StartedToOpen += _blockShootInput.DisableInput;
                x.Closed += _blockShootInput.EnableInput;
            });

            _playWindow.Opened += _blockShootInput.EnableInput;
        }

        public void Dispose()
        {
            _allWindows.ForEach(x =>
            {
                x.StartedToOpen -= _blockShootInput.DisableInput;
                x.Closed -= _blockShootInput.EnableInput;
            });
            
            _playWindow.Opened -= _blockShootInput.EnableInput;
        }
    }
}