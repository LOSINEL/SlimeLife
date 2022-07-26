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
        int amountTmp = _amount;
        if (Item.ItemType.Tool != _item.itemType && Item.ItemType.Weapon != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName && slots[i].itemAmount < _item.bundleSize)
                    {
                        if (slots[i].item.bundleSize < _amount + slots[i].itemAmount)
                        {
                            amountTmp -= slots[i].item.bundleSize - slots[i].itemAmount;
                            break;
                        }
                        else
                        {
                            slots[i].AddItem(_item, _amount);
                            return;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                if (_item.bundleSize < amountTmp)
                {
                    slots[i].AddItem(_item, _item.bundleSize);
                    amountTmp -= _item.bundleSize;
                }
                else
                {
                    slots[i].AddItem(_item, amountTmp);
                    return;
                }
            }
        }
    }

    public void RefreshGold()
    {
        goldText.text = Player.instance.Gold.ToString() + "G";
    }
    public bool IsSlotFull(Item _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null) return false;
            if (slots[i].item.itemName == _item.itemName && slots[i].itemAmount < _item.bundleSize) return false;
        }
        return true;
    }
    public int HowManyItemCanPutIn(Item _item)
    {
        int itemNum = 0;
        for(int i=0;i<slots.Length;i++)
        {
            if (slots[i].item == null) itemNum += _item.bundleSize;
            if (slots[i].item.itemName == _item.itemName)
            {
                itemNum += _item.bundleSize - slots[i].itemAmount;
            }
        }
        return itemNum;
    }
}