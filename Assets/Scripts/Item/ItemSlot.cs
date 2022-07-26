using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    public int itemAmount = 0;
    public Image itemImage;
    [SerializeField] Text itemAmountText;
    [SerializeField] GameObject itemAmountImage;
    float imageAlpha;
    public SlotType slotType;
    public float ImageAlpha { get { return imageAlpha; } }
    public enum SlotType
    {
        Inventory, // �κ��丮 ����
        Weapon, // ���� ����
        Tool, // ���� ����
        Shoes, // �Ź� ����
        Shop, // ���� ����
    }

    public void SetColor(float alpha)
    {
        imageAlpha = alpha;
        itemImage.color = new Color(1, 1, 1, imageAlpha);
    }

    public void AddItem(Item _item, int _amount = 1)
    {
        this.item = _item;
        itemAmount += _amount;
        itemImage.sprite = item.itemImage;
        if (item.itemType != Item.ItemType.Tool && item.itemType != Item.ItemType.Weapon)
        {
            itemAmountImage.SetActive(true);
            itemAmountText.text = itemAmount.ToString();
        }
        else
        {
            itemAmountText.text = "0";
            itemAmountImage.SetActive(false);
        }
        SetColor(1);
    }

    public void SetSlotAmount(int amount)
    {
        itemAmount = amount;
        itemAmountText.text = itemAmount.ToString();
        if (itemAmount <= 0)
        {
            ClearSlot();
        }else
        {
            SetColor(1);
        }
    }

    public void ClearSlot()
    {
        item = null;
        itemAmount = 0;
        itemImage.sprite = null;
        SetColor(0);

        itemAmountText.text = "0";
        itemAmountImage.SetActive(false);
    }
}