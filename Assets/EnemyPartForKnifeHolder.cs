using CodeBase.Gameplay.Character;
using CodeBase.Gameplay.MaterialChanger;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class EnemyPartForKnifeHolder : SerializedMonoBehaviour
{ 
    [field: OdinSerialize] public IDamageable Enemy { get; private set; }
    [field: OdinSerialize] public IMaterialChanger MaterialChanger { get; private set; }
}