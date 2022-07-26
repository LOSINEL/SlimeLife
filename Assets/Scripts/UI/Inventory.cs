using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject InventoryBackground;
    [SerializeField] GameObject InventorySlotsParent;
    public Text goldText;
    ItemSlot[] slots;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = InventoryBackground.GetComponent<RectTransform>();
        slots = InventorySlotsParent.GetComponentsInChildren<ItemSlot>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TryInventory();
        }
        if(InventoryBackground.activeSelf)
        {
            RefreshGold();
        }
    }

    public void TryInventory()
    {
        InventoryBackground.SetActive(!InventoryBackground.activeSelf);
    }

    public void AcquireItem(Item _item, int _amount = 1)
    {
        if (Item.ItemType.Tool != _item.itemType && Item.ItemType.Weapon != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].AddItem(_item, _amount);
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item);
                return;
            }
        }
    }

    public void RefreshGold()
    {
        goldText.text = Player.instance.Gold.ToString() + "G";
    }
}