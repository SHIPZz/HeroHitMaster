using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    private Vector3 _at;

    [Inject]
    public void Construct(Vector3 at)
    {
        _at = at;
    }

    public class Factory : PlaceholderFactory<Vector3, Player> { }

    private void Awake()
    {
        transform.position = _at;
    }
}