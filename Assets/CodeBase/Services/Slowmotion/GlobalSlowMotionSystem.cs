using System.Collections;
using CodeBase.Services;
using UnityEngine;

public class GlobalSlowMotionSystem
{
    private readonly float _maxTimeScale = 1f;
    private readonly ICoroutineStarter _coroutineStarter;
    private Coroutine _playingSlowMotion;

    public GlobalSlowMotionSystem(ICoroutineStarter coroutineStarter) => 
        _coroutineStarter = coroutineStarter;

    public void StartSlowMotion(float targetTimeScale, float duration, float delayBeforeStart)
    {
        if (_playingSlowMotion != null)
            _coroutineStarter.StopCoroutine(PlayingSlowMotion(0,0,0));

        _playingSlowMotion = _coroutineStarter.StartCoroutine(PlayingSlowMotion(targetTimeScale, duration, delayBeforeStart));
    }

    private IEnumerator PlayingSlowMotion(float targetTimeScale, float duration, float delayBeforeStart)
    {
        yield return new WaitForSeconds(delayBeforeStart);
        Time.timeScale = targetTimeScale;
        yield return new WaitForSeconds(Time.timeScale += (_maxTimeScale / duration) * Time.unscaledDeltaTime);
        Time.timeScale = 1;
        yield return null;
    }
}