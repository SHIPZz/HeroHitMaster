namespace CodeBase.Services.EventSystem
{
    public class WebEventSystem : UnityEngine.EventSystems.EventSystem
    {
        protected override void OnApplicationFocus(bool hasFocus) => base.OnApplicationFocus(true);
    }
}