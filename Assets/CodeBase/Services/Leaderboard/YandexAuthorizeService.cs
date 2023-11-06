using System;
using Agava.YandexGames;

namespace CodeBase.Services.Leaderboard
{
    public class YandexAuthorizeService
    {
        public void RequestAccess(Action authorizedCallback)
        {
            if (!PlayerAccount.IsAuthorized)
            {
                PlayerAccount.Authorize(authorizedCallback);
                
                PlayerAccount.RequestPersonalProfileDataPermission();
            }
        }
    }
}