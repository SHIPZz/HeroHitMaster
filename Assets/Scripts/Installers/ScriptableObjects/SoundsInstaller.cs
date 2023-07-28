using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SoundsInstaller", menuName = "Installers/SoundsInstaller")]
public class SoundsInstaller : ScriptableObjectInstaller<SoundsInstaller>
{
    [SerializeField] private SoundsSettings _soundsSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_soundsSettings);
    }
}