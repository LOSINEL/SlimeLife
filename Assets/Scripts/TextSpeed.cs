using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpeed : MonoBehaviour
{
    public Slider textSpeed;
    void Start()
    {
        textSpeed.value = 0.25f;
        textSpeed.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
    public void ValueChangeCheck()
    {
        Hand.instance.NpcScriptTime = textSpeed.value * 0.5f;
        textSpeed.GetComponentsInChildren<Text>()[1].text = (Mathf.Round((textSpeed.value) * 50) * 0.01f).ToString() + "s";
    }
}