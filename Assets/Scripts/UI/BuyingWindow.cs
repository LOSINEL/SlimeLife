using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingWindow : MonoBehaviour
{
    public Text itemNameText;
    public Text itemPriceText;
    public Text itemInfoText;
    public Image itemImage;
    public Item item;
    public int itemPrice;
    public GameObject inventory;
    public int itemAmount = 1;
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
    }
    public void InitBuyingWindow()
    {
        item = null;
        itemNameText.text = "item name";
        itemPriceText.text = "item price";
        itemInfoText.text = "item info";
        itemImage.sprite = null;
        itemPrice = 0;
    }
    public void BuyItemButtonSelected()
    {
        if (Player.instance.Gold >= itemAmount * itemPrice)
        {
            Player.instance.AddGold(-1 * itemPrice * itemAmount);
            inventory.GetComponent<Inventory>().AcquireItem(item, itemAmount);
            CloseBuyingWindowSelected();
        }
        else
        {
            // 골드가 부족합니다 Alert 출력
            CloseBuyingWindowSelected();
        }
    }
    public void CloseBuyingWindowSelected()
    {
        InitBuyingWindow();
        gameObject.SetActive(false);
    }
}