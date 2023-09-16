using CodeBase.Gameplay.Character.Players;
using UnityEditor;
using UnityEngine;

public class RotatePlayerOnTriggerEnterEditor : UnityEditor.Editor
{
    [DrawGizmo(GizmoType.Pickable | GizmoType.Active | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(RotatePlayerOnTriggerEnter rotatePlayerOnTriggerEnter, GizmoType gizmoType)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(rotatePlayerOnTriggerEnter.transform.position, 1f);
    }
}
