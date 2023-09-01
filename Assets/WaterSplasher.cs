using System;
using CodeBase.Gameplay.Weapons;
using CodeBase.Services.Providers;
using UnityEngine;
using Zenject;

public class WaterSplasher : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    private Plane _plane;
    private Weapon _weapon;
    private IProvider<Weapon> _provider;

    [Inject]
    private void Construct(IProvider<Weapon> provider)
    {
        _provider = provider;
    }

    public void Init()
    {
        _weapon = _provider.Get();
        _weapon.Shooted += TryCreateWaterSplash;
    }

    private void TryCreateWaterSplash(Vector3 startPoint, Vector3 endPoint)
    {
        // Destroy(Instantiate(_effect.gameObject, endPoint, _effect.gameObject.transform.rotation), 5f);
        // if (CheckWater(endPoint))
        // {
        //     Vector3 point = RaycastToVirtualPlane(startPoint, endPoint - startPoint);
        //     _effect.Play();
        //     Destroy(Instantiate(_effect.gameObject, point, _effect.gameObject.transform.rotation), 5f);
        // }
    }

    private bool CheckWater(Vector3 input) =>
        input.y < transform.position.y;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

    public Vector3 RaycastToVirtualPlane(Vector3 startPoint, Vector3 direction)
    {
        Plane plane = new(Vector3.up, transform.position);
        Ray ray = new Ray(startPoint, direction.normalized);

        if (plane.Raycast(ray, out float enter))
        {
            return (startPoint + direction.normalized) * enter;
        }

        return Vector3.zero;
    }
}