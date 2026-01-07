[System.Serializable]
public class SimpleItemSlot
{
    public ItemBase Item;

    public int Quantity;
    public int Index;

    public bool IsEmpty => Item == null || Quantity <= 0;

    public void Initialize(ItemBase item, int quantity, int index)
    {
        Item = item;
        Quantity = quantity;
        Index = index;
    }

    public void Reset()
    {
        Item = null;
        Quantity = 0;
        Index = -1;
    }
}
