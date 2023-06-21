// using UnityEngine;
//
//
// public sealed class CameraOverviewInput : MonoBehaviour
// {
//     [SerializeField] private Camera _targetCamera;
//     [SerializeField] private Transform _targetToFollow;
//     [SerializeField] private float _minDistance = 3.0f;
//     [SerializeField] private float _maxDistance = 9.0f;
//     [SerializeField] private Vector2 _axiesSpeedFactor = new(25f, 25f);
//     private float _currentDistance = 0.0f;
//     private Vector2 _angles;
//
//
//     public Camera CapturedCamera => _targetCamera;
//
//
//     private void Start()
//     {
//         const float StartDistanceFactor = 2.0f;
//
//         _currentDistance = _maxDistance / StartDistanceFactor;
//     }
//
//     private void FixedUpdate()
//     {
//         LookAround(Time.fixedDeltaTime);
//     }
//
//     private void LookAround(float deltaTime)
//     {
//         const float FollowSpeed = 20.0f;
//         const float PitchLimit = 85.0f;
//
//         Vector3 cameraPosition;
//         Transform _cameraTranform;
//
//         if (_targetToFollow is null || _targetCamera is null)
//             return;
//
//         _cameraTranform = _targetCamera.transform;
//
//         _angles.x += Input.GetAxis("Mouse X") * _axiesSpeedFactor.x * Time.deltaTime;
//         _angles.y += Input.GetAxis("Mouse Y") * _axiesSpeedFactor.y * Time.deltaTime;
//         _angles.y = Mathf.Clamp(_angles.y, -PitchLimit, PitchLimit);
//
//         cameraPosition = Quaternion.AngleAxis(_angles.x, Vector3.up)
//                          * Quaternion.AngleAxis(_angles.y, Vector3.right)
//                          * (Vector3.forward * _currentDistance)
//                          + _targetToFollow.position;
//
//         _cameraTranform.position = Vector3.Lerp(_cameraTranform.position, cameraPosition, deltaTime * FollowSpeed);
//
//         _cameraTranform.LookAt(_targetToFollow.position); 
//     }
//
//     public Vector3 GetForwardCameraVector2D(float yawAngle = 0.0f)
//     {
//         return Quaternion.AngleAxis(yawAngle, Vector3.up) 
//                * _targetCamera.transform.position.Get2DNormalTo(_targetToFollow.position);
//     }
// }