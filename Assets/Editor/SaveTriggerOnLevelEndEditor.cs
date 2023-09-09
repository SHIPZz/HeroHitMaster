using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.SaveTriggers;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class SaveTriggerOnLevelEndEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SaveTriggerOnLevelEnd saveTriggerOnLevelEnd, GizmoType gizmoType)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(saveTriggerOnLevelEnd.transform.position, 1f);
        }
    }
}
