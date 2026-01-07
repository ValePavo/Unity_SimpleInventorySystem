using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory Settings")]
    [SerializeField] int _capacity = 35;
    [SerializeField] float _dropRadius = 1.2f;

    [Header("UI")]
    [SerializeField] private GameObject _inventoryCanvas;

    [Header("Input")]
    [SerializeField] InputActionReference _toggleInventoryAction;

    [Header("Player Input")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Action Maps")]
    [SerializeField] private string gameplayActionMap = "Player";
    [SerializeField] private string uiActionMap = "UI";

    private bool _isOpen;

    public BaseInventory PlayerInventory { get; private set; }

    void Awake()
    {
        Instance = this;
        PlayerInventory = new SimpleInventory(_capacity);
    }

    private void OnEnable()
    {
        _toggleInventoryAction.action.performed += OnToggleInventory;
        _toggleInventoryAction.action.Enable();

        PlayerInventory.OnDropItem += DropItem;
    }

    private void OnDisable()
    {
        _toggleInventoryAction.action.performed -= OnToggleInventory;
        _toggleInventoryAction.action.Disable();

        PlayerInventory.OnDropItem -= DropItem;
    }

    private void OnToggleInventory(InputAction.CallbackContext ctx)
    {
        SetInventoryState(!_isOpen);
    }

    private void SetInventoryState(bool open)
    {
        if (_isOpen == open) return;
        _isOpen = open;

        _inventoryCanvas.SetActive(open);

        playerInput.SwitchCurrentActionMap(open ? uiActionMap : gameplayActionMap);

        Cursor.visible = open;
        Cursor.lockState = open ? CursorLockMode.None : CursorLockMode.Locked;

        Time.timeScale = open ? 0f : 1f;
    }

    public void PickupItem(ItemBase item, int amount)
    {
        PlayerInventory.AddItem(item, amount);
    }

    private void DropItem(ItemBase item)
    {
        Vector3 randomOffset = Random.insideUnitSphere;
        randomOffset.y = 0f;
        randomOffset.Normalize();

        Vector3 spawnPos = playerInput.transform.position + randomOffset * _dropRadius;

        Instantiate(item.Prefab, spawnPos, Quaternion.identity);
    }
}
