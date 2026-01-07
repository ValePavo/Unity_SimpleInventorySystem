using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemBase : ScriptableObject
{
    public string ID;
    public string DisplayName;
    public Sprite Icon;
    public bool IsStackable;
    public int MaxStack = 99;
    public GameObject Prefab;
}
