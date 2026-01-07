using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemBase itemData;
    [SerializeField] private int amount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        InventoryManager.Instance.PickupItem(itemData, amount);

        Destroy(gameObject);
    }
}