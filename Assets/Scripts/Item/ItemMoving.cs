using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMoving : MonoBehaviour
{
    PointerEventData pointer = new PointerEventData(EventSystem.current);
    List<RaycastResult> raycastResults = new List<RaycastResult>();
    ItemSlot selectedItem;
    ItemSlot unselectedItem;
    public GameObject holdingItemImage;
    public GameObject droppingItemWindow;
    public GameObject sellingItemWindow;
    bool isHoldItem = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointer.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointer, raycastResults);
            if (raycastResults.Count > 0)
            {
                try
                {
                    selectedItem = raycastResults[1].gameObject.GetComponent<ItemSlot>();
                    if (selectedItem.GetComponent<ItemSlot>().item != null && !IsShopSlot(selectedItem))
                    {
                        holdingItemImage.SetActive(true);
                        isHoldItem = true;
                    }
                }
                catch { }
                raycastResults.Clear();
            }
        }
        if (Input.GetMouseButton(0) && isHoldItem)
        {
            holdingItemImage.GetComponent<Image>().sprite = selectedItem.GetComponentsInChildren<Image>()[1].sprite;
            holdingItemImage.GetComponent<RectTransform>().transform.position =
                Input.mousePosition - new Vector3(holdingItemImage.GetComponentsInParent<RectTransform>()[1].rect.width / 2,
                holdingItemImage.GetComponentsInParent<RectTransform>()[1].rect.height / 2 - holdingItemImage.GetComponentsInParent<RectTransform>()[1].transform.position.y, 0);
        }
        if (Input.GetMouseButtonUp(0) && isHoldItem)
        {
            holdingItemImage.SetActive(false);
            isHoldItem = false;
            pointer.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointer, raycastResults);
            if (raycastResults.Count > 0)
            {
                if (raycastResults[0].gameObject.tag.Equals("Shop") || raycastResults[1].gameObject.tag.Equals("Shop"))
                {
                    if(!selectedItem.item.CanSell)
                    {
                        AlertManager.instance.CreateAlertMessage("판매할 수 없는 아이템입니다.");
                        raycastResults.Clear();
                        return;
                    }
                    sellingItemWindow.SetActive(true);
                    sellingItemWindow.GetComponent<SellingWindow>().SetSellingWindow(selectedItem.item);
                }
                try
                {
                    unselectedItem = raycastResults[1].gameObject.GetComponent<ItemSlot>();
                    if (!IsShopSlot(unselectedItem)) ItemSwap();
                }
                catch { }
                raycastResults.Clear();
            }
            else
            {
                droppingItemWindow.SetActive(true);
                droppingItemWindow.GetComponent<DroppingWindow>().SetDroppingWindow(selectedItem.item);
            }
        }
    }
    void ItemSwap()
    {
        /*
         * 아이템 스왑이 가능한 상황 정리
         * unselectedItem 이 인벤토리 빈칸일 때를 제외하고 selectedItem.item.itemType == unselectedItem.item.itemType 이면 스왑
         * unselectedItem 이 장비창 빈칸일 때는 selectedItem.item.itemType.ToString() == unselectedItem.slotType.ToString() 이면 스왑
         * unselectedItem 이 인벤토리 빈칸일 때는 그냥 스왑
         */
        ItemSlot tmpItem = Object.Instantiate(unselectedItem);
        if (unselectedItem.item == null)
        {
            switch (unselectedItem.slotType)
            {
                case ItemSlot.SlotType.Inventory:
                    ItemOptionInput(selectedItem, unselectedItem);
                    ItemOptionInput(tmpItem, selectedItem);
                    break;
                case ItemSlot.SlotType.Weapon:
                case ItemSlot.SlotType.Shoes:
                    if (selectedItem.item.ItemType.ToString() == unselectedItem.slotType.ToString())
                    {
                        ItemOptionInput(selectedItem, unselectedItem);
                        ItemOptionInput(tmpItem, selectedItem);
                    }
                    break;
                case ItemSlot.SlotType.Tool:
                    if (selectedItem.item.ItemType.ToString() == unselectedItem.slotType.ToString())
                    {
                        ItemOptionInput(selectedItem, unselectedItem);
                        ItemOptionInput(tmpItem, selectedItem);
                    }
                    Player.instance.tool.GetComponent<Tool>().ToolChanged(unselectedItem.item, unselectedItem.item.ToolType);
                    Player.instance.SetDamage(unselectedItem.item.Damage, false);
                    break;
            }
        }
        else
        {
            if (selectedItem.item.ItemType == unselectedItem.item.ItemType)
            {
                ItemOptionInput(selectedItem, unselectedItem);
                ItemOptionInput(tmpItem, selectedItem);
                if(unselectedItem.slotType == ItemSlot.SlotType.Tool)
                {
                    Player.instance.tool.GetComponent<Tool>().ToolChanged(unselectedItem.item, unselectedItem.item.ToolType);
                    Player.instance.SetDamage(unselectedItem.item.Damage, false);
                }
            }
            else
            {
                if (selectedItem.slotType == ItemSlot.SlotType.Inventory && unselectedItem.slotType == ItemSlot.SlotType.Inventory)
                {
                    ItemOptionInput(selectedItem, unselectedItem);
                    ItemOptionInput(tmpItem, selectedItem);
                }
                else
                {
                    AlertManager.instance.CreateAlertMessage("아이템 종류가 서로 다릅니다.");
                }
            }
        }
        Destroy(tmpItem.gameObject);
    }

    void ItemOptionInput(ItemSlot _selectedItem, ItemSlot _unselectedItem)
    {
        _unselectedItem.item = _selectedItem.item;
        _unselectedItem.SetColor(_selectedItem.ImageAlpha);
        _unselectedItem.SetSlotAmount(_selectedItem.itemAmount);
        _unselectedItem.GetComponentsInChildren<Image>()[1].sprite = _selectedItem.GetComponentsInChildren<Image>()[1].sprite;
    }
    bool IsShopSlot(ItemSlot _itemSlot)
    {
        if (_itemSlot.slotType == ItemSlot.SlotType.Shop)
            return true;
        else
            return false;
    }
}