using UnityEditor;
using UnityEngine;

namespace CodeBase.Gameplay.Collision
{
    [CustomEditor(typeof(JumpOnTriggerEntered))]
    public class JumpOnTriggerEnteredEditor : Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(JumpOnTriggerEntered jumpOnTriggerEntered, GizmoType gizmoType)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(jumpOnTriggerEntered.transform.position, 1f);
        }
    }
}