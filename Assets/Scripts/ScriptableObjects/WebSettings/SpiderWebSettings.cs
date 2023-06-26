using Enums;

namespace ScriptableObjects.WebSettings
{
    public class SpiderWebSettings : WebSettings
    {
        private void Awake()
        {
            WebTypeId = WebTypeId.SpiderWeb;
            Speed = 5;
            Damage = 20;
        }
    }
}