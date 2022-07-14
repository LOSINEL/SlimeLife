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
                    if (selectedItem.GetComponent<ItemSlot>().item == null)
                    {
                        isHoldItem = false;
                    }else
                    {
                        isHoldItem = true;
                    }
                }
                catch { }
                raycastResults.Clear();
            }
        }
        if (Input.GetMouseButton(0) && isHoldItem)
        {
            // ���콺 Ŀ�� �ؿ� selectedItem ������ �̹��� ���� 0.5 ������ ���󰡰� �����
        }
        if (Input.GetMouseButtonUp(0) && isHoldItem)
        {
            isHoldItem = false;
            pointer.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointer, raycastResults);
            if (raycastResults.Count > 0)
            {
                unselectedItem = raycastResults[1].gameObject.GetComponent<ItemSlot>();
                ItemSwap();
                raycastResults.Clear();
            }
            else
            {
                // �������� �����ðڽ��ϱ�? �޼��� ���
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
        if (unselectedItem.item == null) // unselectedItem �� ��ĭ�϶�
        {
            switch (unselectedItem.slotType)
            {
                case ItemSlot.SlotType.Inventory: // unselectedItem �� �κ��丮 ��ĭ�϶�
                    ItemOptionInput(selectedItem, unselectedItem); // unselectedItem �� selectedItem ������ ����
                    ItemOptionInput(tmpItem, selectedItem); // selectedItem �� tmpItem = unselectedItem ������ ����
                    break;
                case ItemSlot.SlotType.Weapon: // unselectedItem �� ���â ���� ��ĭ�϶�
                case ItemSlot.SlotType.Tool: // unselectedItem �� ���â ���� ��ĭ�϶�
                case ItemSlot.SlotType.Shoes: // unselectedItem �� ���â �Ź� ��ĭ�϶�
                    if (selectedItem.item.itemType.ToString() == unselectedItem.slotType.ToString())
                    {
                        ItemOptionInput(selectedItem, unselectedItem); // unselectedItem �� selectedItem ������ ����
                        ItemOptionInput(tmpItem, selectedItem); // selectedItem �� tmpItem = unselectedItem ������ ����
                    }
                    break;
            }
        }
        else // unselectedItem �� ��ĭ�� �ƴҶ�
        {
            if (selectedItem.item.itemType == unselectedItem.item.itemType) // unselctedItem �� selectedItem �� item.itemType �� ���� �������϶�
            {
                ItemOptionInput(selectedItem, unselectedItem);
                ItemOptionInput(tmpItem, selectedItem);
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