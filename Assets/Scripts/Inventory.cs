using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject InventoryBackground;
    [SerializeField] GameObject InventorySlotsParent;
    InventorySlot[] slots;

    void Start()
    {
        slots = InventorySlotsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        TryOpenInventory();
    }

    void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            InventoryBackground.SetActive(!InventoryBackground.activeSelf);
        }
    }

    public void TryInventory()
    {
        InventoryBackground.SetActive(!InventoryBackground.activeSelf);
    }

    public void AcquireItem(Item item, int amount = 1)
    {
        if (Item.ItemType.Tool != item.itemType && Item.ItemType.Weapon != item.itemType)
        {
            Debug.Log("1");
            for (int i = 0; i < slots.Length; i++)
            {
                Debug.Log("2");
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