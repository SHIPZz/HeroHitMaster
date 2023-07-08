using System;
using System.Collections.Generic;
using Enums;
using Services.Factories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _itemGroupPrefab;

        private readonly int _itemsPerGroup = 5;

        // private InventoryPresenter _inventoryPresenter;
        private GameObject _scrollView;
        private GameObject _content;
        private GameObject _currentGroup;
        private int _itemCount;

        private Dictionary<WeaponTypeId, Image> _icons = new();

        [Inject]
        private void Construct(UIFactory uiFactory)
        {
            _icons = uiFactory.CreateWeaponIcons();
            SetInitialValues();

            CreateIcons();
        }

        private void SetInitialValues()
        {
            _scrollView = transform.parent.gameObject;
            _content = _scrollView.transform.GetChild(0).gameObject;
            _itemCount = 0;
        }

        private void CreateIcons()
        {
            foreach (var icon in _icons.Values)
            {
                AddToLayoutGroup(icon);
            }
        }

        private void AddToLayoutGroup(Image icon)
        {
            GameObject item = icon.gameObject;

            if (_currentGroup == null || _itemCount % _itemsPerGroup == 0)
            {
                _currentGroup = CreateItemGroup();
            }

            _itemCount++;

            item.transform.SetParent(_currentGroup.transform, false);

            LayoutRebuilder.ForceRebuildLayoutImmediate(_content.GetComponent<RectTransform>());
        }


        private GameObject CreateItemGroup()
        {
            GameObject itemGroup = Instantiate(_itemGroupPrefab, _content.transform);
            HorizontalLayoutGroup horizontalLayoutGroup = itemGroup.GetComponent<HorizontalLayoutGroup>();
            horizontalLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
            LayoutRebuilder.ForceRebuildLayoutImmediate(horizontalLayoutGroup.GetComponent<RectTransform>());

            return itemGroup;
        }
    }
}

// private void RemoveFromLayoutGroup(DynamicItem dynamicItem)
        // {
        //     GameObject item = dynamicItem.gameObject;
        //
        //     foreach (Transform groupTransform in _content.transform)
        //     {
        //         for (int i = 0; i < groupTransform.childCount; i++)
        //         {
        //             Transform itemTransform = groupTransform.GetChild(i);
        //
        //             if (itemTransform.gameObject == item)
        //             {
        //                 Destroy(itemTransform.gameObject);
        //                 _itemCount--;
        //
        //                 if (groupTransform.childCount == 0)
        //                 {
        //                     Destroy(groupTransform.gameObject);
        //                     _currentGroup = null;
        //                 }
        //
        //                 return;
        //             }
        //         }
        //     }
        // }
    

// public class Inventory
// {
//     private readonly List<Cell> _cells = new List<Cell>();
//     private int _count;
//
//     public void AddItem(Item item)
//     {
//         bool itemAdded = false;
//
//         for (int i = 0; i < _cells.Count; i++)
//         {
//             if (_cells[i] != null && _cells[i].Item.Equals(item))
//             {
//                 _cells[i].Quantity++;
//                 itemAdded = true;
//                 break;
//             }
//         }
//
//         if (itemAdded == false)
//         {
//             var cell = new Cell(item, 1);
//             _cells.Add(cell);
//         }
//     }
//
//     public void RemoveItem(Item item)
//     {
//         for (int i = 0; i < _cells.Count; i++)
//         {
//             if (_cells[i] != null && _cells[i].Item.Equals(item))
//             {
//                 if (_cells[i].Quantity > 1)
//                 {
//                     _cells[i].Quantity--;
//                 }
//                 else
//                 {
//                     _cells.RemoveAt(i);
//                 }
//                 break;
//             }
//         }
//     }
//
// }
//
// public class InventoryPresenter
// {
//     public event Action<DynamicItem> ItemAdded;
//     public event Action<DynamicItem> ItemRemoved;
//
//     private readonly Inventory _inventory;
//     
//     public InventoryPresenter(Inventory inventory)
//     {
//         _inventory = inventory;
//     }
//
//     public void AddItemToInventory(DynamicItem dynamicItem)
//     {
//         Item item = ItemDatabase.GetItem(dynamicItem.Index);
//         
//         _inventory.AddItem(item);
//         
//         ItemAdded?.Invoke(dynamicItem);
//     }
//
//     public void RemoveItemFromInventory(DynamicItem dynamicItem)
//     {
//         Debug.Log(dynamicItem);
//         Item item = ItemDatabase.GetItem(dynamicItem.Index);
//         
//         _inventory.RemoveItem(item);
//
//         ItemRemoved?.Invoke(dynamicItem);
//     }
// }