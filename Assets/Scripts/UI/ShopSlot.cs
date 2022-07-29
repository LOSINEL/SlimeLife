using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Text itemNameText;
    public Text itemPriceText;
    public Image itemImage;
    public GameObject buyPopUp;
    Item item;
    private void Start()
    {
        item = gameObject.GetComponentInChildren<ItemSlot>().item;
        itemNameText.text = item.ItemName;
        itemPriceText.text = item.BuyPrice.ToString() + "G";
        itemImage.sprite = item.ItemImage;
    }
    public void BuyButtonSelected()
    {
        buyPopUp.SetActive(true);
        buyPopUp.GetComponent<BuyingWindow>().SetBuyingWindow(item);
    }
}