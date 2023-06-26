using DG.Tweening;
using Services.Providers;
using UnityEngine;

namespace Gameplay.Web
{
    public class WebMovement
    {
        public void Move(Vector3 target, Web web, Vector3 startPosition)
        {
            web.transform.position = startPosition;
            // web.transform.position = point;
            web.transform.DOMove(target, 0.2f);
            // web.transform.Translate(target * 30  * Time.deltaTime);
        }
    }
}