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
                    ItemSelected();
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
        }
        if (Input.GetMouseButtonUp(0) && isHoldItem)
        {
            pointer.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointer, raycastResults);
            if (raycastResults.Count > 0)
            {
                {
                    ItemUnselected();
                    ItemSwap();
                }
                raycastResults.Clear();
            }
        }
    }
    void ItemSelected()
    {
        selectedItem = raycastResults[1].gameObject.GetComponent<ItemSlot>();
    }
    void ItemUnselected()
    {
        unselectedItem = raycastResults[1].gameObject.GetComponent<ItemSlot>();
    }
    void ItemSwap()
    {
        ItemSlot tmpItem = Object.Instantiate(unselectedItem);

        ItemOptionInput(unselectedItem, selectedItem);
        ItemOptionInput(selectedItem, tmpItem);

        Destroy(tmpItem.gameObject);
    }

    void ItemOptionInput(ItemSlot deInputItem, ItemSlot inputItem)
    {
        deInputItem.item = inputItem.item;
        deInputItem.SetColor(inputItem.ImageAlpha);
        deInputItem.SetSlotAmount(inputItem.itemAmount);
        deInputItem.GetComponentsInChildren<Image>()[1].sprite = inputItem.GetComponentsInChildren<Image>()[1].sprite;
    }
}