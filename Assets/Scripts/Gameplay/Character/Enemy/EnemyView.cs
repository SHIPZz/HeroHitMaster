using UnityEngine;

namespace Gameplay.Character.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private U10PS_DissolveOverTime _dissolveOverTime;

        public void Disolve()
        {
            _dissolveOverTime.enabled = true;
        }


    }
}