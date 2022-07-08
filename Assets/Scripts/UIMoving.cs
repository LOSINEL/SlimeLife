using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoving : MonoBehaviour
{
    Vector3 canvasPos, topBarLeftUp, topBarRightDown;
    public GameObject uiCanvas;
    float canvasX, canvasY;
    bool topBarClicked = false;
    public float topBarHeight;
    RectTransform rectTransform;
    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        canvasPos = uiCanvas.GetComponent<RectTransform>().rect.position;
        canvasX = uiCanvas.GetComponent<RectTransform>().rect.width / 2;
        canvasY = uiCanvas.GetComponent<RectTransform>().rect.height / 2;
        TopBarSet();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x - canvasX >= topBarLeftUp.x &&
                Input.mousePosition.x - canvasX <= topBarRightDown.x)
            {
                if (Input.mousePosition.y - canvasY >= topBarRightDown.y &&
                    Input.mousePosition.y - canvasY <= topBarLeftUp.y)
                {
                    topBarClicked = true;
                }
            }
        }
        if (Input.GetMouseButton(0) && topBarClicked)
        {
            gameObject.transform.position = new Vector3(Input.mousePosition.x - canvasX, Input.mousePosition.y - canvasY - rectTransform.rect.height / 2 + uiCanvas.transform.position.y, canvasPos.z);
        }
        if (Input.GetMouseButtonUp(0) && topBarClicked)
        {
            TopBarSet();
            topBarClicked = false;
        }
    }
    void TopBarSet()
    {
        topBarLeftUp = new Vector3(rectTransform.rect.width / -2, rectTransform.rect.height / 2, 0) + rectTransform.transform.position; // »ó´Ü¹Ù ÁÂ»ó´Ü ÁÂÇ¥
        topBarRightDown = new Vector3(rectTransform.rect.width / 2, rectTransform.rect.height / 2 - topBarHeight, 0) + rectTransform.transform.position; // »ó´Ü¹Ù ¿ìÇÏ´Ü ÁÂÇ¥
    }
}