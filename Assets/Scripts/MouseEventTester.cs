using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEventTester : MonoBehaviour
{
    void Update()
    {
        Debug.Log(EventSystem.current.IsPointerOverGameObject());
    }
}