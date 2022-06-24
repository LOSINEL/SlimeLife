using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    Camera uiCamera;
    Canvas uiCanvas;
    RectTransform rectParent;
    RectTransform rectHp;
    [HideInInspector] public Vector3 offset = Vector3.zero;
    [HideInInspector] public Transform targetTransform;

    void Start()
    {
        uiCanvas = GetComponentInParent<Canvas>();
        uiCamera = uiCanvas.worldCamera;
        rectParent = uiCanvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }
    void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
        if (screenPos.z < 0f)
        {
            screenPos *= -1f;
        }
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
        rectHp.localPosition = localPos;
    }
}