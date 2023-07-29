using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BulletsInstaller", menuName = "Installers/BulletsInstaller")]
public class BulletsInstaller : ScriptableObjectInstaller<BulletsInstaller>
{
    [SerializeField] private BulletSettings _bulletSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_bulletSettings);
    }
}