using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingWindow : MonoBehaviour
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
        if(Input.GetKeyDown(KeyCode.Return))
        {
            BuyItemButtonSelected();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseBuyingWindowSelected();
        }
    }
    public void SetBuyingWindow(Item _item)
    {
        item = _item;
        itemNameText.text = _item.ItemName;
        itemPriceText.text = _item.BuyPrice.ToString() + "G";
        itemInfoText.text = _item.ItemInfo;
        itemImage.sprite = _item.ItemImage;
        itemPrice = _item.BuyPrice;
        if (!inventory.GetComponent<Inventory>().IsEquipment(_item))
        {
            itemAmountSlider.gameObject.SetActive(true);
            itemAmountSlider.maxValue = _item.BundleSize;
        }
        else
        {
            itemAmountSlider.gameObject.SetActive(false);
        }
        ValueChangeCheck();
    }

    public void InitBuyingWindow()
    {
        item = null;
        itemNameText.text = "item name";
        itemPriceText.text = "item price";
        itemInfoText.text = "item info";
        itemImage.sprite = null;
        itemPrice = 0;
        itemAmountSlider.value = itemAmountSlider.minValue;
    }
    public void BuyItemButtonSelected()
    {
        if (Player.instance.Gold >= itemAmount * itemPrice)
        {
            if (inventory.GetComponent<Inventory>().HowManyItemCanPutIn(item) >= itemAmount)
            {
                Player.instance.AddGold(-1 * itemPrice * itemAmount);
                inventory.GetComponent<Inventory>().AcquireItem(item, itemAmount);
                CloseBuyingWindowSelected();
            }
            else
            {
                AlertManager.instance.CreateAlertMessage("???????? ?????? ??????????.");
            }
        }
        else
        {
            AlertManager.instance.CreateAlertMessage("?????? ??????????.");
        }
    }
    public void CloseBuyingWindowSelected()
    {
        InitBuyingWindow();
        gameObject.SetActive(false);
    }

    public void ValueChangeCheck()
    {
        itemAmount = (int)itemAmountSlider.value;
        itemAmountText.text = ((int)itemAmountSlider.value).ToString();
    }
}