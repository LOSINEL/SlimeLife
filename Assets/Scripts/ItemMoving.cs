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
        
        unselectedItem.item = selectedItem.item;
        unselectedItem.SetColor(selectedItem.ImageAlpha);
        unselectedItem.SetSlotAmount(selectedItem.itemAmount);
        unselectedItem.GetComponentsInChildren<Image>()[1].sprite = selectedItem.GetComponentsInChildren<Image>()[1].sprite;

        selectedItem.item = tmpItem.item;
        selectedItem.SetColor(tmpItem.ImageAlpha);
        selectedItem.SetSlotAmount(tmpItem.itemAmount);
        selectedItem.GetComponentsInChildren<Image>()[1].sprite = tmpItem.GetComponentsInChildren<Image>()[1].sprite;

        Destroy(tmpItem.gameObject);
    }
}