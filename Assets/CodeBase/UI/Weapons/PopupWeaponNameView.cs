using CodeBase.Services.Providers;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Weapons
{
    public class PopupWeaponNameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        
        private IWorldDataService _worldDataService;
        private WeaponSelectorView _weaponSelectorView;

        [Inject]
        private void Construct(IWorldDataService worldDataService) =>
            _worldDataService = worldDataService;

        private void Awake() => 
            _weaponSelectorView = GetComponent<WeaponSelectorView>();

        private void OnEnable() => 
            _nameText.text = _worldDataService.WorldData.TranslatedWeaponNameData.Names[_weaponSelectorView.WeaponTypeId];
    }
}