using CodeBase.Gameplay.Spawners;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmoType)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawner.transform.position, 1f);
        }
    }
}
