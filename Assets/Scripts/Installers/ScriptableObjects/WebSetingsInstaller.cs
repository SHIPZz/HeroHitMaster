using System.Collections.Generic;
using ScriptableObjects.WebSettings;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "WebSetingsInstaller", menuName = "Installers/WebSetingsInstaller")]
public class WebSetingsInstaller : ScriptableObjectInstaller<WebSetingsInstaller>
{
    [SerializeField] private List<WebSettings> _webSettings;

    public override void InstallBindings()
    {
        foreach (var webSetting in _webSettings)
        {
            Container.BindInstance(webSetting).WithId(webSetting.WeaponTypeId);
        }
    }
}