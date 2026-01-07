using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text _quantityText;
    [SerializeField] private Sprite _defaultSprite;
    private SimpleItemSlot itemStack;

    public void SetItem(SimpleItemSlot stack)
    {
        if (stack.IsEmpty)
        {
            ResetUI();
            return;
        }

        itemStack = stack;
        icon.enabled = !stack.IsEmpty;
        icon.sprite = stack.Item != null ? stack.Item.Icon : null;
        _quantityText.text = itemStack.Quantity.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right)
            return;

        var inventory = InventoryManager.Instance.PlayerInventory as SimpleInventory;
        inventory.RemoveItemAt(itemStack.Index);
    }

    private void ResetUI()
    {
        icon.sprite = _defaultSprite;
        _quantityText.text = "";
    }
}