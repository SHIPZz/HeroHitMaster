using CodeBase.Gameplay.ObjectBodyPart;
using UnityEngine;

namespace CodeBase.Gameplay.Collision
{
    [RequireComponent(typeof(JumpOnTriggerEntered))]
    public class SetActiveJumpTriggerOnGlassDestruction : MonoBehaviour
    {
        [SerializeField] private bool _isActive;
        [SerializeField] private DestroyableObject _glass;
        
        private JumpOnTriggerEntered _jumpOnTriggerEnetered;

        private void Awake() => 
            _jumpOnTriggerEnetered = GetComponent<JumpOnTriggerEntered>();

        private void OnEnable()
        {
            _jumpOnTriggerEnetered.enabled = false;
            _glass.Destroyed += SetActive;
        }

        private void OnDisable() => 
            _glass.Destroyed -= SetActive;

        private void SetActive(DestroyableObjectTypeId obj) => 
            _jumpOnTriggerEnetered.enabled = true;
    }
}