using DG.Tweening;
using UnityEngine;

public static class TransformExtension
{
    public static void LookAtXZ(this UnityEngine.Transform transform, Vector3 point)
    {
        var direction = (point - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public static void LookAtXZ(this UnityEngine.Transform transform, Vector3 point, float speed)
    {
        var direction = (point - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), speed);
    }

    public static void LookAtSmooth(this UnityEngine.Transform transform, Vector3 point, float speed)
    {
        var direction = (point - transform.position).normalized;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), speed);
    }

    public static void ChangeLocalPosition(this Transform transform, float valueX,float valueY, float valueZ, float duration) =>
        transform.DOLocalMove(new Vector3(valueX, valueY, valueZ), duration).SetUpdate(true);

    public static void ChangePosition(this Transform transform, float valueX, float valueY, float valueZ, float duration) =>
        transform.DOMove(new Vector3(valueX, valueY, valueZ), duration).SetUpdate(true);
}