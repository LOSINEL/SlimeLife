using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroppingWindow : MonoBehaviour
{
    const float randRange = 1.5f;
    const float dropHeight = 1f;
    public Text itemNameText;
    public Text itemInfoText;
    public Text itemAmountText;
    public Image itemImage;
    public Item item;
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
            TryDropItem();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDroppingWindowSelected();
        }
    }
    public void SetDroppingWindow(Item _item)
    {
        item = _item;
        itemNameText.text = _item.ItemName;
        itemInfoText.text = _item.ItemInfo;
        itemImage.sprite = _item.ItemImage;
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

    public void InitDroppingWindow()
    {
        item = null;
        itemNameText.text = "item name";
        itemInfoText.text = "item info";
        itemImage.sprite = null;
        itemAmountSlider.value = itemAmountSlider.minValue;
    }
    public void TryDropItem()
    {
        inventory.GetComponent<Inventory>().DeleteItem(item, itemAmount);
        for(int i=0;i<itemAmount;i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-1 * randRange, randRange), dropHeight, Random.Range(-1 * randRange, randRange));
            Instantiate(item.ItemPrefab, Player.instance.transform.position + randomPosition, new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        }
        InitDroppingWindow();
        gameObject.SetActive(false);
    }
    public void CloseDroppingWindowSelected()
    {
        InitDroppingWindow();
        gameObject.SetActive(false);
    }

    public void ValueChangeCheck()
    {
        itemAmount = (int)itemAmountSlider.value;
        itemAmountText.text = ((int)itemAmountSlider.value).ToString();
    }
}