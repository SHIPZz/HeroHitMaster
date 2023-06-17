using Gameplay.Character.Player;
using UnityEngine;

namespace Gameplay.PlayerStateHandler.States
{
    public abstract class State : MonoBehaviour
    {
        protected Animator Animator;
        protected StateId StateId;
        
        public abstract void Enable();

        public abstract void Exit();
    }
}