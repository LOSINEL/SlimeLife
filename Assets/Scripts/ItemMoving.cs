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
            // 마우스 커서 밑에 selectedItem 아이템 이미지 알파 0.5 값으로 따라가게 만들기
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
                // 아이템을 버리시겠습니까? 메세지 출력
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
        if (unselectedItem.item == null) // unselectedItem 이 빈칸일때
        {
            switch (unselectedItem.slotType)
            {
                case ItemSlot.SlotType.Inventory: // unselectedItem 이 인벤토리 빈칸일때
                    ItemOptionInput(selectedItem, unselectedItem); // unselectedItem 에 selectedItem 정보를 넣음
                    ItemOptionInput(tmpItem, selectedItem); // selectedItem 에 tmpItem = unselectedItem 정보를 넣음
                    break;
                case ItemSlot.SlotType.Weapon: // unselectedItem 이 장비창 무기 빈칸일때
                case ItemSlot.SlotType.Tool: // unselectedItem 이 장비창 도구 빈칸일때
                case ItemSlot.SlotType.Shoes: // unselectedItem 이 장비창 신발 빈칸일때
                    if (selectedItem.item.itemType.ToString() == unselectedItem.slotType.ToString())
                    {
                        ItemOptionInput(selectedItem, unselectedItem); // unselectedItem 에 selectedItem 정보를 넣음
                        ItemOptionInput(tmpItem, selectedItem); // selectedItem 에 tmpItem = unselectedItem 정보를 넣음
                    }
                    break;
            }
        }
        else // unselectedItem 이 빈칸이 아닐때
        {
            if (selectedItem.item.itemType == unselectedItem.item.itemType) // unselctedItem 이 selectedItem 과 item.itemType 이 같은 아이템일때
            {
                ItemOptionInput(selectedItem, unselectedItem);
                ItemOptionInput(tmpItem, selectedItem);
            }
            else
            {
                Debug.Log("아이템 변경 불가 - 아이템 타입이 서로 다름");
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