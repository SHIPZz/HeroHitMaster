using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Extensions
{
    public static class GameObjectExtension
    {
        public static void SetKinematic(this MonoBehaviour monoBehaviour, IEnumerable<Rigidbody> rigidbodies, bool isKinematic)
        {
            foreach (var rigidbody in rigidbodies)
                rigidbody.isKinematic = isKinematic;
        }

        public static void SetKinematic(this MonoBehaviour monoBehaviour, Rigidbody rigidbody, bool isKinematic)
        {
            rigidbody.isKinematic = isKinematic;
        }

        public static void SetKinematic(this MonoBehaviour monoBehaviour, IEnumerable<Rigidbody> rigidbodies, bool isKinematic, float delay)
        {
            foreach (var rigidbody in rigidbodies)
                monoBehaviour.StartCoroutine(DisableKinematicCoroutine(rigidbody, delay, isKinematic));
        }

        public static void SetActive(this MonoBehaviour monoBehaviour, GameObject gameObject, bool isActive, float delay) =>
            monoBehaviour.StartCoroutine(DisableCoroutine(gameObject, delay, isActive));

        public static void SetActive(this MonoBehaviour monoBehaviour, IEnumerable<GameObject> gameObjects, bool isActive, float delay)
        {
            foreach (GameObject gameObject in gameObjects)
                monoBehaviour.StartCoroutine(DisableCoroutine(gameObject, delay,isActive));
        }

        public static void ChangeScale(this GameObject gameObject, float targetScale, float duration) =>
            gameObject.transform.DOScale(targetScale, duration).SetUpdate(true);

        private static IEnumerator<WaitForSeconds> DisableCoroutine(GameObject gameObject, float delay, bool isActive)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(isActive);
        }

        private static IEnumerator<WaitForSeconds> DisableKinematicCoroutine(Rigidbody rigidbody, float delay, bool isKinmeatic)
        {
            yield return new WaitForSeconds(delay);
            rigidbody.isKinematic = isKinmeatic;
        }
    }
}