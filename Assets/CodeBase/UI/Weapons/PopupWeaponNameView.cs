using CodeBase.Services.SaveSystems;
using CodeBase.Services.SaveSystems.Data;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Weapons
{
    public class PopupWeaponNameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        
        private ISaveSystem _saveSystem;
        private WeaponSelectorView _weaponSelectorView;

        [Inject]
        private void Construct(ISaveSystem saveSystem) =>
            _saveSystem = saveSystem;

        private void Awake() => 
            _weaponSelectorView = GetComponent<WeaponSelectorView>();

        private async void OnEnable()
        {
            var translatedWeaponNameData = await _saveSystem.Load<TranslatedWeaponNameData>();
            _nameText.text = translatedWeaponNameData.Names[_weaponSelectorView.WeaponTypeId];
        }
    }
}