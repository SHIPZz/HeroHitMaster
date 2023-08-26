using System;
using UnityEngine;

namespace CodeBase.Gameplay.MaterialChanger
{
    public interface IMaterialChanger
    {
        public void Change(Material material);
        public event Action StartedChanged;
        public event Action Completed;
        public bool IsChanging { get; }
    }
}