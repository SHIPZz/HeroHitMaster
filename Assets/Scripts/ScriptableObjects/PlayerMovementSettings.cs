using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerMovementSettings", menuName = "Gameplay/PlayerMovementSettings")]
    public class PlayerMovementSettings : ScriptableObject
    {
        [SerializeField] private float _walkingSpeed = 0.1f;
        [SerializeField] private float _rotationSpeed = 3f;
        [SerializeField] private float _jumpSpeed = 5f;
        [SerializeField] private float _runSpeed = 10f;
        [SerializeField] private float _jumpForce = 5f;

        public float WalkingSpeed => _walkingSpeed;

        public float RotationSpeed => _rotationSpeed;
        
        public float JumpSpeed => _jumpSpeed;
        public float RunSpeed => _runSpeed;
        
        public float JumpForce => _jumpForce;
    }
}