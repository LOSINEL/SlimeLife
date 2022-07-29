using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingWindow : MonoBehaviour
{
    public Text itemNameText;
    public Text itemPriceText;
    public Text itemInfoText;
    public Text itemAmountText;
    public Image itemImage;
    public Item item;
    int itemPrice;
    public GameObject inventory;
    public Slider itemAmountSlider;
    int itemAmount = 1;
    private void Start()
    {
        itemAmountSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TrySellItem();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSellingWindowSelected();
        }
    }
    public void SetSellingWindow(Item _item)
    {
        item = _item;
        itemNameText.text = _item.ItemName;
        itemPriceText.text = _item.SellPrice.ToString() + "G";
        itemInfoText.text = _item.ItemInfo;
        itemImage.sprite = _item.ItemImage;
        itemPrice = _item.SellPrice;
        if (!inventory.GetComponent<Inventory>().IsEquipment(_item))
        {
            itemAmountSlider.gameObject.SetActive(true);
            itemAmountSlider.maxValue = inventory.GetComponent<Inventory>().HowManyItemIHave(_item);
        }
        else
        {
            itemAmountSlider.gameObject.SetActive(false);
        }
        ValueChangeCheck();
    }

    public void InitSellingWindow()
    {
        item = null;
        itemNameText.text = "item name";
        itemPriceText.text = "item price";
        itemInfoText.text = "item info";
        itemImage.sprite = null;
        itemPrice = 0;
        itemAmountSlider.value = itemAmountSlider.minValue;
    }
    public void TrySellItem()
    {
        inventory.GetComponent<Inventory>().DeleteItem(item, itemAmount);
        Player.instance.AddGold(itemPrice * itemAmount);
        InitSellingWindow();
        gameObject.SetActive(false);
    }
    public void CloseSellingWindowSelected()
    {
        InitSellingWindow();
        gameObject.SetActive(false);
    }

    public void ValueChangeCheck()
    {
        itemAmount = (int)itemAmountSlider.value;
        itemAmountText.text = ((int)itemAmountSlider.value).ToString();
    }
}
