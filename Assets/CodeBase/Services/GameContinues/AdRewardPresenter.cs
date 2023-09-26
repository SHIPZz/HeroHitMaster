using System;
using CodeBase.UI.Windows.Death;
using Zenject;

namespace CodeBase.Services.GameContinues
{
    public class AdRewardPresenter : IInitializable, IDisposable
    {
        private readonly AdReward _adReward;
        private readonly ContinueADButtonView _continueADButtonView;

        public AdRewardPresenter(AdReward adReward, ContinueADButtonView continueADButtonView)
        {
            _adReward = adReward;
            _continueADButtonView = continueADButtonView;
        }

        public void Initialize() => 
            _continueADButtonView.Clicked += _adReward.Do;

        public void Dispose() => 
            _continueADButtonView.Clicked -= _adReward.Do;
    }
}