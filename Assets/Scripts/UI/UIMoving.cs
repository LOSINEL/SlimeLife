using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMoving : MonoBehaviour
{
    private GraphicRaycaster graphicRaycaster;

    Vector3 canvasPos, topBarLeftUp, topBarRightDown;
    GameObject uiCanvas;
    float canvasX, canvasY;
    bool topBarClicked = false;
    RectTransform selectedUI;
    [SerializeField] GameObject selectedTopBar;
    void Start()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        canvasPos = gameObject.GetComponent<RectTransform>().position;
        canvasX = gameObject.GetComponent<RectTransform>().rect.width / 2;
        canvasY = gameObject.GetComponent<RectTransform>().rect.height / 2;
        selectedUI = gameObject.GetComponent<RectTransform>();
        TopBarSet();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(ped, results);
            results.Clear();
            if (results.Count <= 0) return;
            if (results[0].gameObject.tag.Equals("TopBar"))
            {
                selectedTopBar = results[0].gameObject;
                TopBarSet();
                results.Clear();
            }
            else
            {
                selectedTopBar = null;
                results.Clear();
                return;
            }
            if (Input.mousePosition.x - canvasX >= topBarLeftUp.x && Input.mousePosition.x - canvasX <= topBarRightDown.x)
            {
                // ���콺 y ��ǥ�� topBar y�� �ּ�/�ִ� ���̿� �ִ°�
                if (Input.mousePosition.y - canvasY >= topBarRightDown.y && Input.mousePosition.y - canvasY <= topBarLeftUp.y)
                {
                    // �Ѵ� �ش�Ǹ� ���콺�� topBar ���� ��ġ��
                    topBarClicked = true;
                }
            }
        }
        if (Input.GetMouseButton(0) && topBarClicked)
        {
            selectedUI.position = new Vector3(Input.mousePosition.x - canvasX, Input.mousePosition.y - canvasY - selectedUI.rect.height / 2 + canvasPos.y + selectedTopBar.GetComponent<RectTransform>().rect.height / 2, canvasPos.z);
        }
        if (Input.GetMouseButtonUp(0) && topBarClicked)
        {
            TopBarSet();
            topBarClicked = false;
        }
    }
    void TopBarSet()
    {
        topBarLeftUp = new Vector3(selectedUI.rect.width / -2, selectedUI.rect.height / 2 - canvasPos.y, 0) + selectedUI.position; // ��ܹ� �»�� ��ǥ
        topBarRightDown = new Vector3(selectedUI.rect.width / 2, selectedUI.rect.height / 2 - selectedTopBar.GetComponent<RectTransform>().rect.height - canvasPos.y, 0) + selectedUI.position; // ��ܹ� ���ϴ� ��ǥ
    }
}