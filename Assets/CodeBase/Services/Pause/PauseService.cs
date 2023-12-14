using System;
using UnityEngine;

namespace CodeBase.Services.Pause
{
    public class PauseService : IPauseService
    {
        public event Action<bool> PauseSet;

        public void Pause()
        {
            Time.timeScale = 0f;
            PauseSet?.Invoke(true);
        }

        public void UnPause()
        {
            Time.timeScale = 1;
            PauseSet?.Invoke(false);
        }
    }
}