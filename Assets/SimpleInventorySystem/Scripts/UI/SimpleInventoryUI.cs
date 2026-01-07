using System.Collections.Generic;
using UnityEngine;

public class SimpleInventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _slotParent;
    [SerializeField] private ItemSlotUI _slotPrefab;

    private List<ItemSlotUI> _slots = new();
    private SimpleInventory _inventory;

    void Start()
    {
        _inventory = InventoryManager.Instance.PlayerInventory as SimpleInventory;
        _inventory.OnInventoryChanged += Refresh;

        foreach (var slot in _inventory.Slots)
        {
            var obj = Instantiate(_slotPrefab, _slotParent);
            if (obj.TryGetComponent<ItemSlotUI>(out var slotUI))
            {
                _slots.Add(slotUI);
                slotUI.SetItem(slot);
            }
        }

    }

    void Refresh(int index)
    {
        _slots[index].SetItem(_inventory.Slots[index]);
    }
}
