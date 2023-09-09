using System;
using CodeBase.Services.Ad;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services.CheckOut
{
    public class AdCheckOutService
    {
        private IAdService _adService;
        private int _watchedAds;
        private bool _canContinue;

        public event Action Succeeded;

        public async void Buy(int adsCount)
        {
            _canContinue = true;

            for (int i = 0; i < adsCount; i++)
            {
                if (_watchedAds == adsCount)
                {
                    Succeeded?.Invoke();
                    break;
                }

                _canContinue = false;

                while (!_canContinue)
                    await UniTask.Yield();

                _adService.PlayLongAd(() => _canContinue = false, () =>
                {
                    _canContinue = true;
                    _watchedAds++;
                });
            }
        }
    }
}