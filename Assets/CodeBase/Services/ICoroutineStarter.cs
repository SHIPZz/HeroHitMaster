using System.Collections;
using UnityEngine;

namespace CodeBase.Services
{
    public interface ICoroutineStarter
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
        void StopCoroutine(IEnumerator enumerator);
    }
}