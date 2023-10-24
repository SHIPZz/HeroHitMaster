using CodeBase.Gameplay.ObjectBodyPart;
using Cysharp.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;

namespace CodeBase.Gameplay.NavMesh
{
    public class NavMeshSurfaceChanger : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface _navMeshSurfaceWithGlass;
        [SerializeField] private NavMeshSurface _navMeshSurfaceWithoutGlass;
        [SerializeField] private DestroyableObject _destroyableObject;
        [SerializeField] private float _changeDelay = 1f;

        private void OnEnable() => 
            _destroyableObject.Destroyed += UpdateData;

        private void OnDisable() => 
            _destroyableObject.Destroyed -= UpdateData;

        private async void UpdateData(DestroyableObjectTypeId obj)
        {
            await UniTask.WaitForSeconds(_changeDelay);
            _navMeshSurfaceWithGlass.enabled = false;
            _navMeshSurfaceWithoutGlass.enabled = true;
        }

    }
}