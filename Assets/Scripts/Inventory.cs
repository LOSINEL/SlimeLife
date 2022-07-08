using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;
    [SerializeField] GameObject InventoryBackground;
    [SerializeField] GameObject InventorySlotsParent;
    InventorySlot[] slots;

    void Start()
    {
        slots = InventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        CloseInventory();
    }

    void Update()
    {
        TryOpenInventory();
    }

    void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;
            if (inventoryActivated) OpenInventory();
            else CloseInventory();
        }
    }

    public void OpenInventory()
    {
        InventoryBackground.SetActive(true);
    }

    public void CloseInventory()
    {
        InventoryBackground.SetActive(false);
    }

    public void AcquireItem(Item item, int amount = 1)
    {
        if (Item.ItemType.Tool != item.itemType && Item.ItemType.Weapon != item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == item.itemName)
                    {
                        slots[i].SetSlotAmount(amount);
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(item, amount);
                return;
            }
        }
    }
}