using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Windows.Popup
{
    public class PopupTimer
    {
        private const float CountdownInterval = 0.5f;

        public event Action<float> Decreased;
        
        public async void Init(float targetTime, float disableDelay)
        {
            await  UniTask.WaitForSeconds(disableDelay);
            
            while (targetTime != 0)
            {
                targetTime--;
                Decreased?.Invoke(targetTime);
                await UniTask.WaitForSeconds(CountdownInterval);
            }
        }
    }
}