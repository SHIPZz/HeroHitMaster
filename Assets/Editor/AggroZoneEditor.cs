using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class AggroZoneEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(AggroZone aggroZone, GizmoType gizmoType)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(aggroZone.transform.position, 1f);
        }
    }
}