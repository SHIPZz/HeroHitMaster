using ScriptableObjects;
using ScriptableObjects.WebSettings;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "WebSetingsInstaller", menuName = "Installers/WebSetingsInstaller")]
public class WebSetingsInstaller : ScriptableObjectInstaller<WebSetingsInstaller>
{
    [SerializeField] private WebSettings _webSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_webSettings);
    }
}