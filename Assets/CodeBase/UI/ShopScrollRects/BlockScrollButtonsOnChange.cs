using System;
using System.Collections.Generic;
using CodeBase.Services.Providers;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.ShopScrollRects
{
    public class BlockScrollButtonsOnChange : IInitializable, IDisposable
    {
        private readonly ScrollRectChanger _scrollRectChanger;
        private readonly List<Button> _buttons = new();

        public BlockScrollButtonsOnChange(ScrollRectChanger scrollRectChanger, IProvider<List<ScrollRectView>> provider)
        {
            _scrollRectChanger = scrollRectChanger;
            provider.Get().ForEach(x => _buttons.Add(x.Button));
        }

        public void Initialize() => 
            _scrollRectChanger.Changed += SetActive;

        public void Dispose() => 
        _scrollRectChanger.Changed -= SetActive;

        private void SetActive(bool isChanged)
        {
            if (isChanged)
            {
                _buttons.ForEach(x => x.interactable = false);
                return;
            }

            _buttons.ForEach(x => x.interactable = true);
        }
    }
}