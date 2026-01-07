using System;

public abstract class BaseInventory
{
    public Action<int> OnInventoryChanged;
    public Action<ItemBase> OnDropItem;

    public abstract void AddItem(ItemBase item, int amount);
    public abstract void RemoveItemAt(int index);
}