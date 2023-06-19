using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    public class Player : MonoBehaviour
    {
        private Vector3 _at;

        [Inject]
        public void Construct(Vector3 at)
        {
            _at = at;
            transform.position = _at;
        }

        public class Factory : PlaceholderFactory<Vector3, Player> { }
    }
}
