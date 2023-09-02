using System;
using CodeBase.Gameplay.Collision;
using CodeBase.Services.Pause;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.SaveSystems
{
    public class SaveTriggerOnLevelEnd : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        private IPauseService _pauseService;

        public event Action PlayerEntered;

        [Inject]
        private void Construct(IPauseService pauseService) => 
            _pauseService = pauseService;

        private void OnEnable() => 
            _triggerObserver.Entered += OnPlayerEntered;

        private void OnDisable() => 
            _triggerObserver.Entered -= OnPlayerEntered;

        private void OnPlayerEntered(Collider obj)
        {
            PlayerEntered?.Invoke();
            _pauseService.Pause();
        }
        
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 1);
        }
    }
}