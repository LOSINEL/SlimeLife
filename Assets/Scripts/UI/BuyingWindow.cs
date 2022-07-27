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
    public int itemPrice;
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
        itemNameText.text = _item.itemName;
        itemPriceText.text = _item.buyPrice.ToString() + "G";
        itemInfoText.text = _item.itemInfo;
        itemImage.sprite = _item.itemImage;
        itemPrice = _item.buyPrice;
        if (!inventory.GetComponent<Inventory>().IsEquipment(_item))
        {
            itemAmountSlider.gameObject.SetActive(true);
            itemAmountSlider.maxValue = _item.bundleSize;
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
                AlertManager.instance.CreateAlertMessage("인벤토리 슬롯이 부족합니다.");
            }
        }
        else
        {
            AlertManager.instance.CreateAlertMessage("골드가 부족합니다.");
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