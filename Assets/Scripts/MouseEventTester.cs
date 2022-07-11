using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEventTester : MonoBehaviour
{
    void Update()
    {
        try
        {
            Debug.Log(EventSystem.current.IsPointerOverGameObject());
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        }
        catch { }
    }
}