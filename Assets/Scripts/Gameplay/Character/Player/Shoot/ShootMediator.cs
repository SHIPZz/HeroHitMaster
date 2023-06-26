using System;
using Constants;
using DG.Tweening;
using Gameplay.Web;
using Services;
using Services.ObjectPool;
using Services.Providers;
using Services.Providers.AssetProviders;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Object = UnityEngine.Object;

public class ShootMediator : ITickable
{
    private ShootHand _shootHand;
    private readonly IInputService _inputService;
    private readonly CameraProvider _cameraProvider;
    private readonly WebMovement _webMovement;
    private readonly Transform _righthand;
    private GameObject _webPrefab;
    private readonly GameObjectPool _gameObjectPool;
    private Web _web;
    private bool _canShoot;

    public ShootMediator(ShootHand shootHand, IInputService inputService, CameraProvider cameraProvider,
        WebMovement webMovement, AssetProvider assetProvider, Transform righthand)
    {
        _shootHand = shootHand;
        _inputService = inputService;
        _cameraProvider = cameraProvider;
        _webMovement = webMovement;
        _righthand = righthand;
        _webPrefab = assetProvider.GetAsset(AssetPath.SpiderWeb);
        _canShoot = true;
        // _inputService.PlayerFire.performed += OnPlayerFirePerformed;
        _gameObjectPool = new GameObjectPool(() => Object.Instantiate(_webPrefab), 30);
    }

    // public void Dispose()
    // {
    //     _inputService.PlayerFire.performed -= OnPlayerFirePerformed;
    // }

    public void Tick()
    {
        if (_inputService.PlayerFire.WasPressedThisFrame() && _canShoot)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = _cameraProvider.Camera.ScreenPointToRay(mousePosition);
            GameObject webGameObject = null;
            Shoot(webGameObject, ray);
            _canShoot = false;

            DOTween.Sequence().AppendInterval(0.3f).OnComplete(() => _canShoot = true);

            // if (Physics.Raycast(ray, out RaycastHit hit))
            // {
            // }
        }
    }

    private void Shoot(GameObject webGameObject, Ray ray)
    {
        webGameObject = _gameObjectPool.Pop();
        _web = webGameObject.GetComponent<Web>();
        Vector3 target = Vector3.zero;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            target = hit.point;
            _webMovement.Move(target, _web, _righthand.position);
        }
        else
        {
            target = ray.GetPoint(100f);
            _webMovement.Move(target, _web, _righthand.position);
        }
        DOVirtual.DelayedCall(0.5f, () => _gameObjectPool.Push(webGameObject));
    }

    // private void Shoot(GameObject webGameObject, RaycastHit hit)
    // {
    //     webGameObject = _gameObjectPool.Pop();
    //     _web = webGameObject.GetComponent<Web>();
    //     _webMovement.Move(hit.point, _web, _righthand.position);
    //     DOVirtual.DelayedCall(0.5f, () => _gameObjectPool.Push(webGameObject));
    // }
}