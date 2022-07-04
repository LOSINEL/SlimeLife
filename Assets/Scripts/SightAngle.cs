using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SightAngle : MonoBehaviour
{
    public Slider sightAngle;
    public Camera mainCamera;
    void Start()
    {
        sightAngle.value = 0.5f;
        sightAngle.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
    public void ValueChangeCheck()
    {
        mainCamera.fieldOfView = sightAngle.value * 40 + 16;
        sightAngle.GetComponentsInChildren<Text>()[1].text = ((int)mainCamera.fieldOfView).ToString();
    }
}