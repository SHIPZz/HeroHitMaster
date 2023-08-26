using CodeBase.Gameplay.Collision;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Gameplay.Character.Players
{
    [RequireComponent(typeof(TriggerObserver))]
    public class EnableNavmeshOnTrigger : MonoBehaviour
    {
        private TriggerObserver _triggerObserver;

        private void Awake() => 
            _triggerObserver = GetComponent<TriggerObserver>();

        private void OnEnable() => 
            _triggerObserver.Entered += OnEntered;

        private void OnDisable() => 
            _triggerObserver.Entered -= OnEntered;

        private void OnEntered(Collider obj)
        {
            if (obj.TryGetComponent(out NavMeshAgent navMeshAgent))
                navMeshAgent.enabled = true;
        }
    }
}