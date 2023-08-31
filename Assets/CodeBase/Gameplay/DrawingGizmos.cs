﻿using UnityEditor;
using UnityEngine;

namespace CodeBase.Gameplay
{
    public class DrawingGizmos : MonoBehaviour
    {
        
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 1);
        }
    }
}