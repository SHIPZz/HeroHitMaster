using System.Collections.Generic;
using Enums;
using ScriptableObjects.WebSettings;
using UnityEngine;

namespace Services
{
    public class WebSettingService
    {
        private List<WebSettings> _webSettings = new List<WebSettings>();

        public WebSettingService()
        {
            FillList();
        }

        public WebSettings Get(WebTypeId webTypeId)
        {
            foreach (var webSetting in _webSettings)
            {
                if(webSetting.WebTypeId == webTypeId)
                {
                    return webSetting;
                }
            }

            return null;
        }

        private void FillList()
        {
            _webSettings.Add(ScriptableObject.CreateInstance<SpiderWebSettings>());
            _webSettings.Add(ScriptableObject.CreateInstance<DarkWebSettings>());
        }
    }
}