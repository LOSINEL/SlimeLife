using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public int itemAmount;
    public Image itemImage;
    [SerializeField] Text itemAmountText;
    [SerializeField] GameObject itemAmountImage;

    void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    public void AddItem(Item item, int amount = 1)
    {
        this.item = item;
        itemAmount = amount;
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
        itemAmount += amount;
        itemAmountText.text = itemAmount.ToString();
        if (itemAmount <= 0)
        {
            ClearSlot();
        }
    }

    void ClearSlot()
    {
        item = null;
        itemAmount = 0;
        itemImage.sprite = null;
        SetColor(0);

        itemAmountText.text = "0";
        itemAmountImage.SetActive(false);
    }
}