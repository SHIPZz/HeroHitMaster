using System;
using UnityEngine;

namespace Gameplay.MaterialChanger
{
    public interface IMaterialChanger
    {
        public void Change(Material material);
        public event Action Changed;
    }
}