using System;
using System.Collections.Generic;
using Constants;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Web
{
    public class WebRotation : MonoBehaviour
    {
        private const float MaxFloorHitDistance = 4;
        private const float MaxWallHitDistance = 1;
        private const float TargetDistanceToFloor = 0.5f;
        private const float TargetDistanceToWall = 0.5f;

        private readonly Vector3 _rotationToFloor = new Vector3(90, 0, 0);
        private readonly Vector3 _rotationToRightWall = new Vector3(0, 90, 0);
        private readonly Vector3 _rotationToLeftWall = new Vector3(0, -90, 0);

        private float _distanceToFloor;
        private bool _isRotating;
        private float _wallDistance;
        private bool _isWallRotating;
        private bool _canRotateToWall;
        private bool _canRotateToFloor;
        private Dictionary<Vector3, Vector3> _rotationTargets;

        private void Awake()
        {
            _rotationTargets = new Dictionary<Vector3, Vector3>()
            {
                { Vector3.left, _rotationToLeftWall },
                { Vector3.right, _rotationToRightWall },
                { Vector3.down, _rotationToFloor },
            };
        }

        private void Update()
        {
            StartToWallRotation(Vector3.left);
            StartToWallRotation(Vector3.right);

            FloorToFloorRotation();

            _canRotateToWall = false;
            _canRotateToFloor = false;
        }

        private void FloorToFloorRotation()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit floorHit, MaxFloorHitDistance))
            {
                _distanceToFloor = floorHit.distance;

                if (floorHit.collider.gameObject.layer == LayerIdConstant.Floor)
                    _canRotateToFloor = true;

                if (_distanceToFloor < TargetDistanceToFloor && !_isRotating && _canRotateToFloor)
                {
                    RotateWeb(_rotationToFloor);
                }
            }
        }

        private void StartToWallRotation(Vector3 target)
        {
            if (Physics.Raycast(transform.position, target, out RaycastHit wallHit, MaxWallHitDistance))
            {
                if (wallHit.collider.gameObject.layer == LayerIdConstant.Wall)
                    _canRotateToWall = true;

                _wallDistance = wallHit.distance;

                if (_wallDistance < TargetDistanceToWall && !_isWallRotating && _canRotateToWall)
                {
                    Vector3 rotationTarget = GetRotationTargetBy(target);
                    Debug.Log(rotationTarget);
                    RotateWebToWall(rotationTarget);
                }
            }
        }

        private Vector3 GetRotationTargetBy(Vector3 target) =>
            _rotationTargets[target];

        private void RotateWeb(Vector3 targetRotation)
        {
            _isRotating = true;

            transform.DORotate(targetRotation, 0.5f)
                .SetEase(Ease.OutQuint)
                .OnComplete(() => { _isRotating = false; });
        }

        private void RotateWebToWall(Vector3 targetRotation)
        {
            _isWallRotating = true;

            transform.DORotate(targetRotation, 0.2f)
                .SetEase(Ease.OutQuint)
                .OnComplete(() => { _isWallRotating = false; });
        }
    }
}