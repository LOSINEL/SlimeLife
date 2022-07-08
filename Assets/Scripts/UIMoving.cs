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
    void Start()
    {
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
            gameObject.transform.position = new Vector3(Input.mousePosition.x - canvasX, Input.mousePosition.y - canvasY - gameObject.GetComponent<RectTransform>().rect.height / 2 + uiCanvas.transform.position.y, canvasPos.z);
        }
        if (Input.GetMouseButtonUp(0) && topBarClicked)
        {
            TopBarSet();
            topBarClicked = false;
        }
    }
    void TopBarSet()
    {
        topBarLeftUp = new Vector3(gameObject.GetComponent<RectTransform>().rect.width / -2, gameObject.GetComponent<RectTransform>().rect.height / 2, 0) + gameObject.GetComponent<RectTransform>().transform.position; // 상단바 좌상단 좌표
        topBarRightDown = new Vector3(gameObject.GetComponent<RectTransform>().rect.width / 2, gameObject.GetComponent<RectTransform>().rect.height / 2 - topBarHeight, 0) + gameObject.GetComponent<RectTransform>().transform.position; // 상단바 우하단 좌표
    }
}