using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public GameObject merchantShopBackground, merchantShopGrayWindow;
    public GameObject npcScriptWindow, inventoryWindow;
    GameObject[] canvasObjects;
    private void Start()
    {
        canvasObjects = GameObject.FindGameObjectsWithTag("Canvas");
    }
    public void OpenMerchantShop()
    {
        for (int i = 0; i < canvasObjects.Length; i++)
        {
            if (canvasObjects[i].name.Equals("InventoryCanvas"))
            {
                inventoryWindow.SetActive(true);
                continue;
            }
            canvasObjects[i].SetActive(false);
        }
        npcScriptWindow.SetActive(false);
        merchantShopBackground.SetActive(true);
        merchantShopGrayWindow.SetActive(true);
        Hand.instance.somethingOpen = true;
    }
    public void CloseMerchantShop()
    {
        for(int i=0;i<canvasObjects.Length;i++)
        {
            canvasObjects[i].SetActive(true);
        }
        npcScriptWindow.SetActive(true);
        merchantShopBackground.SetActive(false);
        merchantShopGrayWindow.SetActive(false);
        Hand.instance.somethingOpen = false;
    }
}