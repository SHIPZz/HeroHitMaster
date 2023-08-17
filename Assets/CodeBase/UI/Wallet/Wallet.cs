using System;
using UnityEngine;

namespace CodeBase.UI.Wallet
{
    public class Wallet
    {
        private int _money = 1000;
        private int _maxMoney;

        public event Action<int> MoneyChanged;

        public void SetMaxMoney(int maxMoney) =>
            _maxMoney = maxMoney;
        
        public void AddMoney(int money)
        {
            _money = Mathf.Clamp(_money + money, 0, _maxMoney);
            
            MoneyChanged?.Invoke(_money);
        }

        public bool TryRemoveMoney(int money)
        {
            if (_money - money < 0)
                return false;

            _money -= money;
            
            MoneyChanged?.Invoke(_money);
            return true;
        } 
    }
}