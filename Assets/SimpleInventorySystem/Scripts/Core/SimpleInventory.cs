using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SimpleInventory : BaseInventory
{
    protected readonly List<SimpleItemSlot> _slots;
    public IReadOnlyList<SimpleItemSlot> Slots => _slots;


    public SimpleInventory(int capacity)
    {
        _slots = new List<SimpleItemSlot>();

        for (int i = 0; i < capacity; i++)
        {
            _slots.Add(new SimpleItemSlot());
        }
    }

    public override void AddItem(ItemBase item, int amount)
    {
        if (item == null || amount <= 0) return;

        int remaining = amount;

        if (item.IsStackable)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                if (_slots[i].Item == item && _slots[i].Quantity < item.MaxStack)
                {
                    int space = item.MaxStack - _slots[i].Quantity;
                    int toAdd = Mathf.Min(space, remaining);

                    _slots[i].Quantity += toAdd;
                    remaining -= toAdd;

                    OnInventoryChanged?.Invoke(i);

                    if (remaining <= 0) break;
                }
            }
        }

        if (remaining > 0)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                if (_slots[i].IsEmpty)
                {
                    int toAdd = Mathf.Min(item.MaxStack, remaining);
                    _slots[i].Initialize(item, toAdd, i);
                    remaining -= toAdd;

                    OnInventoryChanged?.Invoke(i);

                    if (remaining <= 0) break;
                }
            }
        }
    }

    public int GetTotalQuantity(ItemBase item)
    {
        int total = 0;
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].Item == item) total += _slots[i].Quantity;
        }
        return total;
    }

    public override void RemoveItemAt(int index)
    {
        if (index < 0 || index >= _slots.Count)
            return;

        var item = _slots[index];
        item.Quantity--;

        OnDropItem?.Invoke(item.Item);

        if (item.Quantity <= 0)
            _slots[index].Reset();

        OnInventoryChanged?.Invoke(index);
    }
}
