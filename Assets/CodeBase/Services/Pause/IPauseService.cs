using System;

namespace CodeBase.Services.Pause
{
    public interface IPauseService
    {
        event Action<bool> PauseSet; 
        void Pause();
        void UnPause();
    }
}