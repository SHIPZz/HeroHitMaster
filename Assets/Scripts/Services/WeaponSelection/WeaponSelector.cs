using Databases;
using Enums;
using UnityEngine;

public class WeaponSelector
{
    private GameObject _webPrefab;
    private readonly WeaponsProvider _weaponsProvider;
    private int _currentWeaponId;

    public WeaponSelector(WeaponsProvider weaponsProvider)
    {
        _weaponsProvider = weaponsProvider;
        // var weapon = Resources.Load<GameObject>(AssetPath.SpiderWebGun);
        // var comp = Object.Instantiate(weapon).GetComponent<IWeapon>();
        // _weaponsProvider.Add(comp);
        // var newweapon = Resources.Load<GameObject>(AssetPath.WolverineWebGun);
        // var newcomp = Object.Instantiate(newweapon).GetComponent<IWeapon>();
        // _weaponsProvider.Add(newcomp);
        // _weaponsProvider.CurrentWeapon = _weaponsProvider.Weapons[Enums.WeaponTypeId.ShootSpiderHand];
    }

    public WeaponTypeId WeaponTypeId { get; private set; }

    public void SelectNextWeapon()
    {
        _currentWeaponId++;

        if (_currentWeaponId >= _weaponsProvider.Weapons.Count)
            _currentWeaponId = 0;

        _weaponsProvider.CurrentWeapon.GameObject.SetActive(false);

        SetActiveWeapon();
    }

    public void SelectPreviousWeapon()
    {
        _currentWeaponId--;

        if (_currentWeaponId < 0)
            _currentWeaponId = _weaponsProvider.Weapons.Count - 1;

        _weaponsProvider.CurrentWeapon.GameObject.SetActive(false);

        SetActiveWeapon();
        
    }

    private void SetActiveWeapon()
    {
        // OldWeaponSwitched?.Invoke(_currentWeapon);

        WeaponTypeId = (WeaponTypeId)_currentWeaponId;
        _weaponsProvider.CurrentWeapon = _weaponsProvider.Weapons[WeaponTypeId];
        _weaponsProvider.CurrentWeapon.GameObject.SetActive(true);

        // ChoosedWeapon?.Invoke(_currentWeapon);
    }
}