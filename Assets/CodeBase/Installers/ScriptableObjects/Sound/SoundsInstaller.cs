using CodeBase.ScriptableObjects.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SoundsInstaller", menuName = "Installers/SoundsInstaller")]
    public class SoundsInstaller : ScriptableObjectInstaller<SoundsInstaller>
    {
        [SerializeField] private SoundsSettings _soundsSettings;
    
        public override void InstallBindings()
        {
            Container.BindInstance(_soundsSettings);
        }
    }
}