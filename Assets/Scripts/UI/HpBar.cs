using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    Camera hpCamera;
    Canvas hpCanvas;
    RectTransform rectParent;
    RectTransform rectHp;
    [HideInInspector] public Vector3 offset = Vector3.zero;
    [HideInInspector] public Transform targetTransform;

    void Start()
    {
        hpCanvas = GetComponentInParent<Canvas>();
        hpCamera = hpCanvas.worldCamera;
        rectParent = hpCanvas.GetComponent<RectTransform>();
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, hpCamera, out localPos);
        rectHp.localPosition = localPos;
    }
}