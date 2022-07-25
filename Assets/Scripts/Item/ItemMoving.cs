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
                    if (selectedItem.GetComponent<ItemSlot>().item != null)
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
                try
                {
                    unselectedItem = raycastResults[1].gameObject.GetComponent<ItemSlot>();
                    ItemSwap();
                }
                catch { }
                raycastResults.Clear();
            }
            else
            {
                droppingItemWindow.SetActive(true);
            }
        }
    }
    void ItemSwap()
    {
        /*
         * ������ ������ ������ ��Ȳ ����
         * unselectedItem �� �κ��丮 ��ĭ�� ���� �����ϰ� selectedItem.item.itemType == unselectedItem.item.itemType �̸� ����
         * unselectedItem �� ���â ��ĭ�� ���� selectedItem.item.itemType.ToString() == unselectedItem.slotType.ToString() �̸� ����
         * unselectedItem �� �κ��丮 ��ĭ�� ���� �׳� ����
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
                    if (selectedItem.item.itemType.ToString() == unselectedItem.slotType.ToString())
                    {
                        ItemOptionInput(selectedItem, unselectedItem);
                        ItemOptionInput(tmpItem, selectedItem);
                    }
                    break;
                case ItemSlot.SlotType.Tool:
                    if (selectedItem.item.itemType.ToString() == unselectedItem.slotType.ToString())
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
            if (selectedItem.item.itemType == unselectedItem.item.itemType)
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
                Debug.Log("������ ���� �Ұ� - ������ Ÿ���� ���� �ٸ�");
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
}