using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [SerializeField] GameObject InventoryBackground;
    [SerializeField] GameObject InventorySlotsParent;
    public Text goldText;
    ItemSlot[] slots;
    RectTransform rectTransform;

    private void Awake()
    {
        instance = this;
    }
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
        if (Item.ItemType_.Tool != _item.ItemType && Item.ItemType_.Weapon != _item.ItemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.ItemName == _item.ItemName && slots[i].itemAmount < _item.BundleSize)
                {
                    // 슬롯에 있는 아이템이 집어넣으려는 아이템과 같고 번들사이즈보다 적을 때
                    if (slots[i].itemAmount + amountTmp <= _item.BundleSize)
                    {
                        // 넣으려는 아이템의 수량을 더해도 번들사이즈보다 적을 때
                        slots[i].SetSlotAmount(slots[i].itemAmount + amountTmp);
                        return;
                    }
                    else
                    {
                        // 넣으려는 아이템의 수량을 더하면 번들사이즈보다 클 
                        amountTmp -= _item.BundleSize - slots[i].itemAmount;
                        slots[i].SetSlotAmount(_item.BundleSize);
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                if (_item.BundleSize < amountTmp)
                {
                    slots[i].AddItem(_item, _item.BundleSize);
                    amountTmp -= _item.BundleSize;
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
            if (slots[i].item.ItemName == _item.ItemName && slots[i].itemAmount < _item.BundleSize) return false;
        }
        return true;
    }
    public int HowManyItemCanPutIn(Item _item)
    {
        int itemNum = 0;
        for(int i=0;i<slots.Length;i++)
        {
            if (slots[i].item == null)
            {
                itemNum += _item.BundleSize;
                continue;
            }
            if (slots[i].item.ItemName == _item.ItemName)
            {
                itemNum += _item.BundleSize - slots[i].itemAmount;
            }
        }
        return itemNum;
    }
    public int HowManyItemIHave(Item _item)
    {
        int itemNum = 0;
        for(int i = 0; i < slots.Length;i++)
        {
            if (slots[i].item == null) continue;
            if (slots[i].item.ItemName == _item.ItemName)
            {
                itemNum += slots[i].itemAmount;
            }
        }
        return itemNum;
    }
    public bool IsEquipment(Item _item)
    {
        if (_item.ItemType == Item.ItemType_.Tool) return true;
        if (_item.ItemType == Item.ItemType_.Weapon) return true;
        if (_item.ItemType == Item.ItemType_.Shoes) return true;
        return false;
    }
    public void DeleteItem(Item _item, int _amount = 1)
    {
        int itemNum = _amount;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemAmount == 0) continue;
            if (slots[i].item.ItemName == _item.ItemName)
            {
                if (itemNum < slots[i].itemAmount)
                {
                    slots[i].itemAmount -= _amount;
                    slots[i].SetSlotAmount(slots[i].itemAmount);
                    return;
                }
                else
                {
                    itemNum -= slots[i].itemAmount;
                    slots[i].ClearSlot();
                }
            }
        }
    }
}